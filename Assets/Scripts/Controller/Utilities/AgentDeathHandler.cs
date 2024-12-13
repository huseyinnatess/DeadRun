using ObjectPools;
using UnityEngine;

namespace Controller.Utilities
{
    public static class AgentDeathHandler
    {
        public static void DeathHandel(Transform agentTransform)
        {
            ParticleEffectPool.Instance.DeadEffectPool(agentTransform);
            DeathStainPool.Instance.DeathStainObjectPool(true, agentTransform);
            agentTransform.gameObject.SetActive(false);
            CheckWar.CheckWarResult();
        }
    }
}