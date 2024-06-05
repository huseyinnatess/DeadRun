using ObjectPools;
using Utilities;

namespace Controller.Utilities
{
    public static class CheckWar
    {
        /// <para> Savaş sonucunu kontrol eder. </para> 
        public static void CheckWarResult()
        {
            if ((EnemyController.IsCanAttack &&
                 (AgentPools.Instance.AgentCount == 0 || EnemyController.EnemyAgentCount == 0)) ||
                AgentPools.Instance.AgentCount == 0)
            {
                Battlefield.Instance.WarResult(AgentPools.Instance.AgentCount);
            }
        }
    }
}