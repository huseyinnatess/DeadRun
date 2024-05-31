using System;
using System.Collections;
using Manager;
using MonoSingleton;
using ObjectPools;
using UnityEngine;

namespace Controller
{
    public class CharacterControl : MonoSingleton<CharacterControl>
    {
        private float _inputAxis;
        private float _speed;

        private bool _isCanRun;
        private bool _isTouchingColumn;
        private bool _isTouchingLeftBorder;
        private bool _isTouchingRightBorder;
            
        private Animator _animator;

        private void Awake()
        {
            AgentPools.Instance.AddMainCharacter(gameObject);
            _animator = GetComponent<Animator>();
            _speed = 1f;
            _isCanRun = false;
            _isTouchingColumn = false;
            _isTouchingLeftBorder = false;
            _isTouchingRightBorder = false;
        }

        private void Start()
        {
            SetEnemyTarget();
        }

        #region Update, FixedUpdate

        private void FixedUpdate()
        {
            if (_speed != 0f)
                SetCharacterPosition();
        }

        private void Update()
        {
            if (_isTouchingColumn == false)
                CharacterMovement(_speed);
            GetInputAxis();
            ActivateRunAnimation();
        }
        private void ActivateRunAnimation()
        {
            if (GameManager.IsSetActiveHandle != 1 || _isCanRun) return;
            _animator.SetTrigger("IsCanRun");
            _isCanRun = true;
        }

        private void CharacterMovement(float speed)
        {
            transform.Translate(Vector3.forward * (speed * Time.deltaTime * GameManager.IsSetActiveHandle));
        }

        private void GetInputAxis()
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

        private void SetCharacterPosition()
        {
            var position = transform.position;
            position = Vector3.Lerp(position, new Vector3(position.x - _inputAxis, position.y, position.z), .3f);
            transform.position = position;
        }

        #endregion

        public void SetVictoryAnimation()
        {
            _speed = 0f;
            _animator.SetTrigger("Victory");
        }
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
                EnemyController.Attack(true);
            }

            if (AgentPools.Instance.CharacterCount == 1 && (other.CompareTag("ThornBox") || other.CompareTag("Saw") ||
                                                            other.CompareTag("ThornWall") || other.CompareTag("Hammer") ||
                                                            other.CompareTag("EnemyAgent")))
            {
                gameObject.SetActive(false);
                AgentPools.Instance.CharacterCount--;
                ParticleEffectPool.Instance.DeadEffectPool(transform);
                DeathStainPool.Instance.DeathStainObjectPool(true, transform);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Column"))
            {
                _isTouchingColumn = true;
                CharacterMovement(0);
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

        private void SetEnemyTarget()
        {
            EnemyController.Target = transform;
        }
    }
}