using ObjectPools;
using UnityEngine;

namespace Controller.Utilities
{
    public class AgentTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("ThornBox") || other.CompareTag("Saw") || other.CompareTag("ThornWall") || other.CompareTag("Hammer"))
            {
                gameObject.SetActive(false);
                AgentPools.Instance.AgentCount--;
                ParticleEffectPool.Instance.DeadEffectPool(transform);
                DeathStainPool.Instance.DeathStainObjectPool(true, transform);
            }

            if (other.CompareTag("EnemyAgent"))
            {
                gameObject.SetActive(false);
                AgentPools.Instance.AgentCount--;
                ParticleEffectPool.Instance.DeadEffectPool(transform);
                DeathStainPool.Instance.DeathStainObjectPool(true, transform);
                EnemyController.EnemyAgentCount--;
                other.gameObject.SetActive(false);
            }

            if (other.CompareTag("Battlefield"))
            {
                AgentController.Instance.Target = EnemyController.Instance.GetActiveEnemy();
            }
        }
    }
}