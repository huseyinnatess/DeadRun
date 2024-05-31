using ObjectPools;
using UnityEngine;
using UnityEngine.AI;

namespace Controller
{
    public class AgentController : MonoBehaviour
    {
        private Transform _target;

        private NavMeshAgent _navMesh;
        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            _target = GameObject.FindWithTag("DestiniationPos").transform;
            _navMesh = GetComponent<NavMeshAgent>();
        }

        private void LateUpdate()
        { 
            _navMesh.SetDestination(_target.position);
            if (_target is not null)
                LookAtEnemy();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("ThornBox") || other.CompareTag("Saw") || other.CompareTag("ThornWall") || other.CompareTag("Hammer"))
            {
                gameObject.SetActive(false);
                AgentPools.Instance.CharacterCount--;
                ParticleEffectPool.Instance.DeadEffectPool(transform);
                DeathStainPool.Instance.DeathStainObjectPool(true, transform);
            }

            if (other.CompareTag("EnemyAgent"))
            {
                gameObject.SetActive(false);
                AgentPools.Instance.CharacterCount--;
                ParticleEffectPool.Instance.DeadEffectPool(transform);
                DeathStainPool.Instance.DeathStainObjectPool(true, transform);
                EnemyController.EnemyAgentCount--;
                other.gameObject.SetActive(false);
            }

            if (other.CompareTag("Battlefield"))
            {
                _target = EnemyController.Instance.GetActiveEnemy();
            }
        }
        private void LookAtEnemy()
        {
            transform.LookAt(_target.position, Vector3.up);
        }
    }
}
