using Controller.AgentsController;
using Manager.Audio.Utilities;
using ObjectPools;
using UnityEngine;

namespace Controller.Utilities
{
    public class AgentTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("ThornBox") || other.CompareTag("Saw") || other.CompareTag("ThornWall") ||
                other.CompareTag("Hammer"))
            {
                AgentPools.Instance.AgentCount--;
                FxSounds.Instance.DeadAgentFx.Play();
                AgentDeathHandler.DeathHandel(transform);
            }

            if (other.CompareTag("EnemyAgent"))
            {
                AgentPools.Instance.AgentCount--;
                EnemyController.EnemyAgentCount--;
                other.gameObject.SetActive(false);
                FxSounds.Instance.DeadAgentFx.Play();
                AgentController.Instance.Target = EnemyController.Instance.GetActiveEnemy();
                AgentDeathHandler.DeathHandel(transform);
            }

            if (other.CompareTag("Battlefield"))
            {
                AgentController.Instance.Target = EnemyController.Instance.GetActiveEnemy();
            }
        }
    }
}