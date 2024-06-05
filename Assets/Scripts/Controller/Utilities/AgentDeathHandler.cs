using ObjectPools;
using UnityEngine;

namespace Controller.Utilities
{
    public static class AgentDeathHandler
    {
        /// <para> Parametre olarak gelen agent'ın pozisyonunda ölüm efektlerini çalıştırıp inaktif yapar.</para>
        /// <para> Agent azaltma işlemi fonksiyon çağrısından önce yapılmalı.</para>
        /// <para> Savaş sonucu kontrolü yapılıyor. </para> 
        public static void DeathHandel(Transform agentTransform)
        {
            ParticleEffectPool.Instance.DeadEffectPool(agentTransform);
            DeathStainPool.Instance.DeathStainObjectPool(true, agentTransform);
            agentTransform.gameObject.SetActive(false);
            CheckWar.CheckWarResult();
        }
    }
}