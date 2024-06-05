using System.Linq;
using MonoSingleton;
using UnityEngine;

namespace Controller
{
    public class EnemyController : MonoSingleton<EnemyController>
    {
        public static bool IsCanAttack; // Enemy'nin saldırıya başlayacağını belirleyen değişken
        public static int EnemyAgentCount; // Level'daki enemy sayısı
        public GameObject[] EnemyAgents; // Enemy'lerin tutulduğu array

        private Transform _target; // Enemy'ler için hedef
        private Animator[] _enemyAnimators; // Enemey'lerin animator componentini tutan array
        private bool _attackAnimationWork; // Enemy'lerin saldırı animasyonuna geçmesini belirleyen değişken
        
        private float _enemySpeed; // Enemy'lerin hızı

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

        #region LateUpdate

        private void LateUpdate()
        {
            if (_target is not null && IsCanAttack)
                EnemyAttack();
            if (_target is not null && !_target.gameObject.activeInHierarchy)
                _target = AgentController.GetActiveAgent();
        }

        #endregion
        
        
        // LateUpdate
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
            _enemyAnimators[i].SetBool("Attack", true);
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