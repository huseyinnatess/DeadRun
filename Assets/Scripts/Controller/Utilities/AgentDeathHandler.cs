using ObjectPools;
using UnityEngine;

namespace Controller.Utilities
{
    public class AgentDeathHandler
    {
        // Parametre olarak gelen agent'ın pozisyonunda ölüm efektlerini çalıştırıp
        // inaktif yapar.
        public static void DeathHandel(Transform agentTransform)
        {
            ParticleEffectPool.Instance.DeadEffectPool(agentTransform);
            DeathStainPool.Instance.DeathStainObjectPool(true, agentTransform);
            agentTransform.gameObject.SetActive(false);
        }
    }
}