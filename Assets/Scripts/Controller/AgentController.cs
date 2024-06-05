using MonoSingleton;
using ObjectPools;
using UnityEngine;
using UnityEngine.AI;

namespace Controller
{
    public class AgentController : MonoSingleton<AgentController>
    {
       [HideInInspector] public Transform Target; // Navmesh için target

        private NavMeshAgent[] _navMeshAgents; // Tüm agent'ların navmesh componentlerini tutan array
        private int _agentsCount; // Anlık agent sayısı
        
        #region Awake Get, Set Functions
        private void Awake()
        {
            GetReferences();
            SetReferences();
        }

        private void GetReferences()
        {
            Target = GameObject.FindWithTag("DestiniationPos").transform;
        }
        
        // Alt obje olarak bulunan tüm agentların ilgili componentini arraye alıyor
        private void SetReferences()
        {
            _agentsCount = transform.childCount;
            _navMeshAgents = new NavMeshAgent[_agentsCount];
            for (int i = 0; i < _agentsCount; i++)
                _navMeshAgents[i] = transform.GetChild(i).GetComponent<NavMeshAgent>();
        }

        #endregion

        #region LateUpdate
        private void LateUpdate()
        {
            SetAgentTarget();
        }
        
        // LateUpdate
        // Agent'ların target'ını ayarlıyor.
        private void SetAgentTarget()
        {
            for (int i = 0; i < _agentsCount; i++)
            {
                if (transform.GetChild(i).gameObject.activeInHierarchy && Target is not null)
                    _navMeshAgents[i].SetDestination(Target.position);
            }
        }

        #endregion
        
        /// <summary>
        /// Sahnede aktif olan agent'ın transform'unu return eder.
        /// </summary> 
        public static Transform GetActiveAgent()
        {
            int i = 0;
            while (i < AgentPools.Instance.Agents.Count)
            {
                if (AgentPools.Instance.Agents[i].activeInHierarchy)
                    return AgentPools.Instance.Agents[i].transform;
                i++;
            }
            return null;
        }
        
        /// <summary>
        /// Parent'a yeni obje eklenince componentleri günceller.
        /// </summary>
        public void UpdateAgentsComponent()
        {
            SetReferences();
        }
    }
}
