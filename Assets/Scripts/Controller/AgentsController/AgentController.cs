using MonoSingleton;
using ObjectPools;
using UnityEngine;
using UnityEngine.AI;

namespace Controller.AgentsController
{
    public class AgentController : MonoSingleton<AgentController>
    {
       [HideInInspector] public Transform Target;

        private NavMeshAgent[] _navMeshAgents;
        private int _agentsCount;
        
        #region Start Get, Set Functions
        private void Start()
        {
            GetReferences();
            SetReferences();
        }

        private void GetReferences()
        {
            Target = GameObject.FindWithTag("DestiniationPos").transform;
        }
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
        private void SetAgentTarget()
        {
            for (int i = 0; i < _agentsCount; i++)
            {
                if (transform.GetChild(i).gameObject.activeInHierarchy && Target is not null)
                    _navMeshAgents[i].SetDestination(Target.position);
            }
        }

        #endregion
        
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
        
        public void UpdateAgentsComponent()
        {
            SetReferences();
        }
    }
}
