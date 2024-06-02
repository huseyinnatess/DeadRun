using System.Linq;
using MonoSingleton;
using UnityEngine;
using UnityEngine.AI;

namespace Controller
{
    public class EnemyController : MonoSingleton<EnemyController>
    {
        public static bool IsCanAttack;
        public static int EnemyAgentCount;
        public GameObject[] EnemyAgents;

        private Transform _target;
        private Animator[] _enemyAnimator;
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
            _enemyAnimator = new Animator[EnemyAgents.Length];
            for (int i = 0; i < EnemyAgents.Length; i++)
            {
                if (!EnemyAgents[i].activeInHierarchy) continue;
                _enemyAnimator[i] = EnemyAgents[i].GetComponent<Animator>();
            }
        }

        private void SetReferences()
        {
            IsCanAttack = false;
            _attackAnimationWork = false;
            _target = CharacterController.Instance.transform;
            EnemyAgentCount = EnemyAgents.Length;
            _enemySpeed = 1f;
        }

        #endregion
        
        // Hiyerarşide aktif olan ilk enemy'nin transformunu return eder.
        public Transform GetActiveEnemy()
        {
            return EnemyAgents.FirstOrDefault(agent => agent.activeInHierarchy)?.transform;
        }

        // Enemy'lerin saldıraya başlamasını sağlar.
        public static void Attack(bool attack) => IsCanAttack = attack;
        
        private void LateUpdate()
        {
            if (_target is not null && IsCanAttack)
                EnemyAttack();
            if (_target is not null && !_target.gameObject.activeInHierarchy)
                _target = AgentController.Instance.GetActiveAgent();
        }
        
        // Enemylerin agentlar'a saldırı yapmasını başlatır.
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
        
        // Enemy'nin attack animasyonunu başlatır
        private void SetAttackAnimations(int i)
        {
            _enemyAnimator[i].SetBool("Attack", true);
        }

        // Target ve enemy arasındaki farkı return eder.
        private Vector3 GetDirectionToTarget(int i)
        {
            return (_target.position - EnemyAgents[i].transform.position).normalized;
        }
        
        // Enemy'lerin yönlerini target'a ayarlar.
        private void LookTarget(int i)
        {
            EnemyAgents[i].transform.LookAt(_target, Vector3.up);
        }
    }
}