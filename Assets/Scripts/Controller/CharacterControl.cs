using System;
using Controller.Utilities;
using Manager;
using Manager.Audio.Utilities;
using MonoSingleton;
using ObjectPools;
using UnityEngine;
using UnityEngine.AI;

namespace Controller
{
    public class CharacterControl : MonoSingleton<CharacterControl>
    {
        private float _inputAxis; // Mouse hareketine göre karakterin gideceği yön
        private float _characterSpeed; // Karakterin hızı

        private bool _isCanRun; // Karakter koşmaya başladı mı?
        private bool _isTouchingColumn; // Karakter numaralı panellerin columnlarına çarptı mı?
        private bool _isTouchingLeftBorder; // Karakter zeminin sol sınırına çarptı mı?
        private bool _isTouchingRightBorder; // Karakter zeminin sağ sınırına çarptı mı?

        private Animator _animator;

        private NavMeshAgent _navMeshAgent;
        private Transform _enemyTarget; // NavmeshAgent için hedef

        [Header("NumbersPanel")] 
        private char _sign; // Sayısal panelin işareti
        private int _count; // Sayısal panelde işaretten sonra gelen sayı

        #region Awake, Get, Set Functions

        private void Awake()
        {
            AgentPools.Instance.AddMainCharacter(gameObject);
            GetReferences();
            SetReferences();
        }

        private void GetReferences()
        {
            _animator = GetComponent<Animator>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _enemyTarget = EnemyController.Instance.GetActiveEnemy();
        }

        private void SetReferences()
        {
            _characterSpeed = 1f;
            _isCanRun = false;
            _isTouchingColumn = false;
            _isTouchingLeftBorder = false;
            _isTouchingRightBorder = false;
        }

        #endregion
        
        #region Update, FixedUpdate, LateUpdate

        private void FixedUpdate()
        {
            if (_characterSpeed != 0f)
                SetCharacterDirection(transform.position);
        }

        private void Update()
        {
            if (!Warning.CharacterCanMove || GameManager.HandelIsActive) return;
            if (_isTouchingColumn == false)
                StabilizeForwardMovement(_characterSpeed);
            if (_isCanRun == false)
                SetRunAnimation();
            SetInputAxis();
        }

        private void LateUpdate()
        {
            if (!_navMeshAgent.enabled) return;
            SetCharacterNavMesh();
            if (_enemyTarget is not null)
                LookAtEnemy();
        }
        #endregion
        
        // Update
        // Mouse tıklandığı zaman karakterin Run animasyonunu başlatıyor.
        private void SetRunAnimation()
        {
            _animator.SetTrigger("IsCanRun");
            if (FxSounds.Instance.CharacterRunFx.isPlaying)
                FxSounds.Instance.CharacterRunFx.Play();
            _isCanRun = true;
        }

        // Update
        // Karakterin koşarken sola kaymasını minimuma indiriyor.
        private void StabilizeForwardMovement(float speed)
        {
            transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        }

        // Update
        // Karakterin Mouse yönüne bağlı olarak sağa sola gitmesini sağlıyor.
        private void SetInputAxis()
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (Input.GetAxis("Mouse X") < 0 && _isTouchingLeftBorder == false)
                {
                    FxSounds.Instance.MoveSoundFx.Play();
                    _inputAxis = .1f;
                }
                if (Input.GetAxis("Mouse X") > 0 && _isTouchingRightBorder == false)
                {
                    FxSounds.Instance.MoveSoundFx.Play();
                    _inputAxis = -.1f;
                }
            }
            else
                _inputAxis = 0f;
        }

        // FixedUpdate
        // Karakterin koşarken " _inputAxis'e" göre yönünü ayarlıyor.
        private void SetCharacterDirection(Vector3 target)
        {
            transform.position = Vector3.Lerp(target, new Vector3(target.x - _inputAxis, target.y, target.z), .3f);
        }
        #region OnTriggerEnter

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("NumbersPanel"))
            {
                _sign = Convert.ToChar(other.name.Remove(1, 1));
                _count = Convert.ToInt32(other.name.Remove(0, 1));
                AgentPools.Instance.AgentObjectPoolManager(_sign, _count, other.transform);
            }

            if (other.CompareTag("Battlefield"))
            {
                _characterSpeed = 0f;
                _navMeshAgent.enabled = true;
                EnemyController.Attack(true);
            }

            if (_navMeshAgent.enabled && other.CompareTag("EnemyAgent"))
            {
                AgentPools.Instance.AgentCount--;
                EnemyController.EnemyAgentCount--;
                other.gameObject.SetActive(false);
                AgentController.Instance.Target = EnemyController.Instance.GetActiveEnemy();
                AgentDeathHandler.DeathHandel(transform);
            }
        }


        private void OnTriggerStay(Collider other)
        {
            // Eğer oyunda sadece anakarakter kaldı ise ölme işlemini tetikliyor.
            if (AgentPools.Instance.AgentCount == 1 && (other.CompareTag("ThornBox") || other.CompareTag("Saw") ||
                                                        other.CompareTag("ThornWall") || other.CompareTag("Hammer")))
            {
                AgentPools.Instance.AgentCount--;
                Warning.Instance.enabled = false;
                AgentDeathHandler.DeathHandel(transform);
            }
        }

        #endregion
        

        #region OnCollision Functions

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Column"))
            {
                _isTouchingColumn = true;
                StabilizeForwardMovement(0);
            }

            if (other.gameObject.CompareTag("LeftBorder"))
            {
                _isTouchingLeftBorder = true;
                _inputAxis = 0f;
            }

            if (other.gameObject.CompareTag("RightBorder"))
            {
                _isTouchingRightBorder = true;
                _inputAxis = 0f;
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.CompareTag("Column"))
            {
                _isTouchingColumn = false;
            }

            if (other.gameObject.CompareTag("LeftBorder"))
            {
                _isTouchingLeftBorder = false;
            }

            if (other.gameObject.CompareTag("RightBorder"))
            {
                _isTouchingRightBorder = false;
            }
        }


        #endregion
        
        // LateUpdate
        // Karakter bitiş çizgisini geçtiği zaman NavMesh Agent componenti enabled edilip target'ı ayarlanıyor.
        private void SetCharacterNavMesh()
        {
            _enemyTarget = EnemyController.Instance.GetActiveEnemy();
            if (_enemyTarget is null) return;
            _navMeshAgent.SetDestination(_enemyTarget.position);
        }

        // LateUpdate
        // Karakter bitiş çizgisini geçtiği zaman enemy'e bakmasını sağlıyor.
        private void LookAtEnemy()
        {
            transform.LookAt(_enemyTarget.position, Vector3.up);
        }
    }
}