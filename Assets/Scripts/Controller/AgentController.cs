using MonoSingleton;
using UnityEngine;
using UnityEngine.AI;

namespace Controller
{
    public class AgentController : MonoSingleton<AgentController>
    {
        public Transform Target;

        private NavMeshAgent[] _navMeshAgent;
        private int AgentsCount;
        
        #region Awake

        private void Awake()
        {
            GetReferences();
            SetRefernces();
        }

        private void GetReferences()
        {
            Target = GameObject.FindWithTag("DestiniationPos").transform;
        }

        private void SetRefernces()
        {
            AgentsCount = transform.childCount;
            _navMeshAgent = new NavMeshAgent[AgentsCount * 2];
            for (int i = 0; i < AgentsCount - 1; i++)
                _navMeshAgent[i] = transform.GetChild(i).GetComponent<NavMeshAgent>();
        }

        #endregion

        #region LateUpdate
        
        private void LateUpdate()
        {
            for (int i = 0; i < AgentsCount; i++)
            {
                if (transform.GetChild(i).gameObject.activeInHierarchy)
                    _navMeshAgent[i].SetDestination(Target.position);
            }
           // if (Target is not null)
           //      LookAtEnemy();
        }

        #endregion
        
        
        
        // // LateUpdate
        // // Bitiş çizgisini geçtiklerinde düşmana doğru bakmalarını sağlıyor.
        // private void LookAtEnemy()
        // {
        //     transform.LookAt(Target.position, Vector3.up);
        // }
    }
}
