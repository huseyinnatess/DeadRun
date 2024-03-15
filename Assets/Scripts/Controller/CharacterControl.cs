using System;
using ObjectPools;
using UnityEngine;

namespace Controller
{
    public class CharacterControl : MonoBehaviour
    {
        private float _inputAxis;

        private void Awake()
        {
            AgentPools.Instance.AddMainCharacter(gameObject);
        }

        private void CharacterMovement()
        {
            transform.Translate(Vector3.forward * (1f * Time.deltaTime));
        }
        private void FixedUpdate()
        {
            SetCharacterPosition();
        }

        private void Update()
        {
            CharacterMovement();
            GetInputAxis();
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
                other.CompareTag("ThornWall") || other.CompareTag("Hammer") || other.CompareTag("EnemyAgent")))
            {
                gameObject.SetActive(false);
                AgentPools.CharacterCount--;
                ParticleEffectPool.Instance.DeadEffectPool(transform);
                DeathStainPool.Instance.DeathStainObjectPool(true, transform);
            }
        }
    }
}


