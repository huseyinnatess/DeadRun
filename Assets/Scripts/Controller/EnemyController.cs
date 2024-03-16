using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Controller
{
    public class EnemyController : MonoBehaviour
    {
        public static bool IsCanAttack;
        public static int EnemyAgentCount;
        public GameObject[] EnemyAgents;

        private Transform _target;
        private NavMeshAgent[] _enemyNavMeshAgent;
        private Animator[] _enemyAnimator;
        private bool _attackAnimationWork;

        #region Awake Get, Set Funcstions

        private void Awake()
        {
            GetReferences();
            GetEnemyComponent();
            SetReferences();
        }

        private void GetReferences()
        {
            _target = GameObject.FindWithTag("Character").transform;
        }

        private void GetEnemyComponent()
        {
            _enemyNavMeshAgent = new NavMeshAgent[EnemyAgents.Length];
            _enemyAnimator = new Animator[EnemyAgents.Length];
            for (int i = 0; i < EnemyAgents.Length; i++)
            {
                if (!EnemyAgents[i].activeInHierarchy) continue;
                _enemyNavMeshAgent[i] = EnemyAgents[i].GetComponent<NavMeshAgent>();
                _enemyAnimator[i] = EnemyAgents[i].GetComponent<Animator>();
            }
        }

        private void SetReferences()
        {
            IsCanAttack = false;
            _attackAnimationWork = false;
            EnemyAgentCount = EnemyAgents.Length;
        }

        #endregion

        public static void Attack(bool attack) => IsCanAttack = attack;
        
        private void LateUpdate()
        {
            EnemyAttack();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void EnemyAttack()
        {
            if (IsCanAttack)
            {
                for (int i = 0; i < EnemyAgents.Length; i++)
                {
                    if (EnemyAgents[i].activeInHierarchy)
                    {
                        if (!_attackAnimationWork)
                            _enemyAnimator[i].SetBool("Attack", true);
                        _enemyNavMeshAgent[i].SetDestination(_target.transform.position);
                    }
                }

                _attackAnimationWork = true;
            }
        }
    }
}