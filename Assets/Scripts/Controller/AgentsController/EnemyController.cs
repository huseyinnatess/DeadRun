using System.Linq;
using MonoSingleton;
using UnityEngine;

namespace Controller.AgentsController
{
    public class EnemyController : MonoSingleton<EnemyController>
    {
        public static bool IsCanAttack;
        public static int EnemyAgentCount;
        public GameObject[] EnemyAgents;

        private Transform _target;
        private Animator[] _enemyAnimators;
        private bool _attackAnimationWork;

        private float _enemySpeed;

        #region Awake Get, Set Functions

        private void Awake()
        {
            GetEnemyComponent();
            SetReferences();
        }

        private void GetEnemyComponent()
        {
            _enemyAnimators = EnemyAgents
                .Where(agent => agent.activeInHierarchy)
                .Select(agent => agent.GetComponent<Animator>())
                .ToArray();
        }

        private void SetReferences()
        {
            IsCanAttack = false;
            _attackAnimationWork = false;
            _target = CharacterControl.Instance.transform;
            EnemyAgentCount = EnemyAgents.Length;
            _enemySpeed = 1.5f;
        }

        #endregion


        public Transform GetActiveEnemy() => EnemyAgents.FirstOrDefault(agent => agent.activeInHierarchy)?.transform;


        public static void Attack(bool attack) => IsCanAttack = attack;

        #region LateUpdate

        private void LateUpdate()
        {
            if (_target is not null && IsCanAttack)
                EnemyAttack();
            if (_target is not null && !_target.gameObject.activeInHierarchy)
                _target = AgentController.GetActiveAgent();
        }

        #endregion


        private void EnemyAttack()
        {
            for (int i = 0; i < EnemyAgents.Length; i++)
            {
                if (!EnemyAgents[i].activeInHierarchy) continue;
                if (!_attackAnimationWork)
                    SetAttackAnimations(i);
                LookTarget(i);
                EnemyAgents[i].transform.position += GetDirectionToTarget(i) * (_enemySpeed * Time.deltaTime);
            }

            _attackAnimationWork = true;
        }

        private void SetAttackAnimations(int i) => _enemyAnimators[i].SetBool("Attack", true);

        private Vector3 GetDirectionToTarget(int i) =>
            (_target.position - EnemyAgents[i].transform.position).normalized;

        private void LookTarget(int i) => EnemyAgents[i].transform.LookAt(_target, Vector3.up);
    }
}