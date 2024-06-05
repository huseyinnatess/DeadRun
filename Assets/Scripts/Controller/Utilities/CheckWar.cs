using ObjectPools;
using Utilities;

namespace Controller.Utilities
{
    public static class CheckWar
    {
        /// <summary>
        /// Savaş sonucunu kontrol eder.
        /// </summary>
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