using MonoSingleton;
using UnityEngine;
using UnityEngine.AI;

namespace Controller
{
    public class AgentController : MonoSingleton<AgentController>
    {
       [HideInInspector] public Transform Target;

        private NavMeshAgent[] _navMeshAgents;
        private Animator[] _animators;
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
            _navMeshAgents = new NavMeshAgent[AgentsCount];
            _animators = new Animator[AgentsCount];
            for (int i = 0; i < AgentsCount; i++)
            {
                _navMeshAgents[i] = transform.GetChild(i).GetComponent<NavMeshAgent>();
                _animators[i] = transform.GetChild(i).GetComponent<Animator>();
            }
        }

        #endregion

        #region LateUpdate
        
        private void LateUpdate()
        {
            for (int i = 0; i < AgentsCount; i++)
            {
                if (transform.GetChild(i).gameObject.activeInHierarchy && Target is not null)
                    _navMeshAgents[i].SetDestination(Target.position);
                if (transform.GetChild(i).gameObject.activeInHierarchy && EnemyController.IsCanAttack
                                                                       && !_animators[i].applyRootMotion)
                    _animators[i].applyRootMotion = true;
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
        
        // Parent'a yeni obje eklenince componentleri günceller
        public void UpdateAgentsComponent()
        {
            SetReferences();
        }
    }
}
