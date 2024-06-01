using ObjectPools;
using UnityEngine;

namespace Controller.Utilities
{
    public class AgentDeathHandler
    {
        public static void DeathHandel(Transform agentTransform)
        {
            ParticleEffectPool.Instance.DeadEffectPool(agentTransform);
            DeathStainPool.Instance.DeathStainObjectPool(true, agentTransform);
            agentTransform.gameObject.SetActive(false);
        }
    }
}