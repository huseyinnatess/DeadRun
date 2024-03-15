using ObjectPools;
using UnityEngine;
using UnityEngine.AI;

namespace Controller
{
    public class AgentController : MonoBehaviour
    {
        private Transform _destiniation;

        private NavMeshAgent _navMesh;
        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            _destiniation = GameObject.FindWithTag("DestiniationPos").transform;
            _navMesh = GetComponent<NavMeshAgent>();
        }

        private void LateUpdate()
        {
            Debug.Log(_destiniation.position);
            NavMeshSetDestiniation();
        }

        private void NavMeshSetDestiniation()
        {
            _navMesh.SetDestination(_destiniation.position);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("ThornBox") || other.CompareTag("Saw") || other.CompareTag("ThornWall") || other.CompareTag("Hammer"))
            {
                gameObject.SetActive(false);
                AgentPools.CharacterCount--;
                ParticleEffectPool.Instance.DeadEffectPool(transform);
                DeathStainPool.Instance.DeathStainObjectPool(true, transform);
            }

            if (other.CompareTag("EnemyAgent"))
            {
                gameObject.SetActive(false);
                AgentPools.CharacterCount--;
                ParticleEffectPool.Instance.DeadEffectPool(transform);
                DeathStainPool.Instance.DeathStainObjectPool(true, transform);
                EnemyController.EnemyAgentCount--;
                other.gameObject.SetActive(false);
            }
        }
    }
}
