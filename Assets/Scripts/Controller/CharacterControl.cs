using System;
using Controller.Utilities;
using Manager;
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
        private void Awake()
        {
            AgentPools.Instance.AddMainCharacter(gameObject);
            _animator = GetComponent<Animator>();
            _characterSpeed = 1f;
            _isCanRun = false;
            _isTouchingColumn = false;
            _isTouchingLeftBorder = false;
            _isTouchingRightBorder = false;
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _enemyTarget = EnemyController.Instance.GetActiveEnemy();
        }
        #region Update, FixedUpdate
        
        private void FixedUpdate()
        {
            if (_characterSpeed != 0f)
                SetCharacterDirection(transform.position);
        }
        
        private void Update()
        {
            if (_isTouchingColumn == false)
                StabilizeForwardMovement(_characterSpeed);
            if (_isCanRun == false)
                SetRunAnimation();
            SetInputAxis();
            

        }

        private void LateUpdate()
        {
            if (_navMeshAgent.enabled)
            {
                SetCharacterNavMesh();
                if (_enemyTarget is not null)
                    LookAtEnemy();
            }
        }
        
        // Update
        // Mouse tıklandığı zaman karakterin Run animasyonunu başlatıyor.
        private void SetRunAnimation()
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                _animator.SetTrigger("IsCanRun");
                _isCanRun = true;
            }
        }
        
        // Update
        // Karakterin koşarken sola kaymasını minimuma indiriyor.
        private void StabilizeForwardMovement(float speed)
        {
            transform.Translate(Vector3.forward * (speed * Time.deltaTime * GameManager.IsSetActiveHandle));
        }
        
        // Update
        // Karakterin Mouse yönüne bağlı olarak sağa sola gitmesini sağlıyor.
        private void SetInputAxis()
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (Input.GetAxis("Mouse X") < 0 && _isTouchingLeftBorder == false)
                    _inputAxis = .1f;
                if (Input.GetAxis("Mouse X") > 0 && _isTouchingRightBorder == false)
                    _inputAxis = -.1f;
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

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("NumbersPanel"))
            {
                char sign = Convert.ToChar(other.name.Remove(1, 1));
                if (sign == 'x' || sign == '+' || sign == '-' || sign == '/')
                {
                    int count = Convert.ToInt32(other.name.Remove(0, 1));
                    AgentPools.Instance.AgentObjectPoolManager(sign, count, other.transform);
                }
            }

            if (other.CompareTag("Battlefield"))
            {
                _characterSpeed = 0f;
                _navMeshAgent.enabled = true;
                EnemyController.Attack(true);
            }
            // Eğer oyunda sadece anakarakter kaldı ise ölme işlemini tetikliyor.
            if (AgentPools.Instance.AgentCount == 1 && (other.CompareTag("ThornBox") || other.CompareTag("Saw") ||
                                                            other.CompareTag("ThornWall") || other.CompareTag("Hammer")))
            {
                AgentDeathHandler.DeathHandel(transform);
                AgentPools.Instance.AgentCount--;
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