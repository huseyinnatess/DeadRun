using ObjectPools;
using UnityEngine;

namespace Controller.Utilities
{
    public static class AgentDeathHandler
    {
        /// <summary>
        /// Parametre olarak gelen agent'ın pozisyonunda ölüm efektlerini çalıştırıp inaktif yapıyor.
        /// Agent azaltma işlemi fonksiyon çağrısından önce yapılmalı.
        /// Aynı zamanda savaş sonucu kontrolü yapılıyor.
        /// </summary>
        /// <param name="agentTransform"> Yok edilecek agent'ın transform component'i</param>
        public static void DeathHandel(Transform agentTransform)
        {
            ParticleEffectPool.Instance.DeadEffectPool(agentTransform);
            DeathStainPool.Instance.DeathStainObjectPool(true, agentTransform);
            agentTransform.gameObject.SetActive(false);
            CheckWar.CheckWarResult();
        }
    }
}