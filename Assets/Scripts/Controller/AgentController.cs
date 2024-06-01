using MonoSingleton;
using UnityEngine;
using UnityEngine.AI;

namespace Controller
{
    public class AgentController : MonoSingleton<AgentController>
    {
       [HideInInspector] public Transform Target;

        private NavMeshAgent[] _navMeshAgent;
        private int AgentsCount;
        
        #region Awake

        private void Awake()
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
            AgentsCount = transform.childCount;
            Debug.Log(AgentsCount);
            _navMeshAgent = new NavMeshAgent[AgentsCount * 2];
            for (int i = 0; i < AgentsCount; i++)
                _navMeshAgent[i] = transform.GetChild(i).GetComponent<NavMeshAgent>();
        }

        #endregion

        #region LateUpdate
        
        private void LateUpdate()
        {
            for (int i = 0; i < AgentsCount; i++)
            {
                if (transform.GetChild(i).gameObject.activeInHierarchy && Target is not null)
                {               
                    _navMeshAgent[i].SetDestination(Target.position);
                }
            }
        }
        #endregion
        
        // Sahnede aktif olan agent'ın transform'unu return eder.
        public Transform GetActiveAgent()
        {
            int i = 0;

            while (i < AgentsCount)
            {
                if (transform.GetChild(i).gameObject.activeInHierarchy)
                    return transform.GetChild(i).transform;
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
