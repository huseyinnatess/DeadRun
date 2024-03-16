using System;
using MonoSingleton;
using ObjectPools;
using UnityEngine;

namespace Controller
{
    public class CharacterControl : MonoSingleton<CharacterControl>
    {
        private float _inputAxis;
        private Animator _animator;
        private float _speed;
        private void Awake()
        {
            AgentPools.Instance.AddMainCharacter(gameObject);
            _animator = GetComponent<Animator>();
            _speed = 1f;
        }

        #region Update, FixedUpdate

        private void FixedUpdate()
        {
            if (_speed != 0f)
                SetCharacterPosition();
        }

        private void Update()
        {
            CharacterMovement();
            GetInputAxis();
        }

        private void CharacterMovement()
        {
            transform.Translate(Vector3.forward * (_speed * Time.deltaTime));
        }

        private void GetInputAxis()
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (Input.GetAxis("Mouse X") < 0)
                    _inputAxis = .1f;
                if (Input.GetAxis("Mouse X") > 0)
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

            if (AgentPools.CharacterCount == 1 && (other.CompareTag("ThornBox") || other.CompareTag("Saw") ||
                                                   other.CompareTag("ThornWall") || other.CompareTag("Hammer") ||
                                                   other.CompareTag("EnemyAgent")))
            {
                gameObject.SetActive(false);
                AgentPools.CharacterCount--;
                ParticleEffectPool.Instance.DeadEffectPool(transform);
                DeathStainPool.Instance.DeathStainObjectPool(true, transform);
            }
        }
    }
}