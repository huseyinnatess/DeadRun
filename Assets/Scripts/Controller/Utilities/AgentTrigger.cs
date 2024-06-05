using ObjectPools;
using UnityEngine;

namespace Controller.Utilities
{
    public class AgentTrigger : MonoBehaviour
    {
        // Agent'lar içerisinde bulunan ontrigger enter fonksiyonu
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("ThornBox") || other.CompareTag("Saw") || other.CompareTag("ThornWall") || other.CompareTag("Hammer"))
            {
                AgentPools.Instance.AgentCount--;
                AgentDeathHandler.DeathHandel(transform);
            }

            if (other.CompareTag("EnemyAgent"))
            {
                AgentPools.Instance.AgentCount--;
                EnemyController.EnemyAgentCount--;
                other.gameObject.SetActive(false);
                AgentController.Instance.Target = EnemyController.Instance.GetActiveEnemy();
                AgentDeathHandler.DeathHandel(transform);
            }
            
            // Finish çizgisi geçildikten sonra ilk enemy'i target olarak ayarlıyor.
            if (other.CompareTag("Battlefield"))
            {
                AgentController.Instance.Target = EnemyController.Instance.GetActiveEnemy();
            }
        }
    }
}