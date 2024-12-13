using System.Collections;
using Manager;
using UnityEngine;

namespace Controller.ObstacleController
{
    public class HammerController : MonoBehaviour
    {
        private Animator _animator;
        private Transform _character;

        private float _minDistance;
        private float _maxDistance;

        #region Start

        private void Awake()
        {
            _minDistance = 5f;
            _maxDistance = -2f;
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _character = GameObject.FindWithTag("Character").transform;
            StartCoroutine(HammerAnimation());
        }

        #endregion

        private IEnumerator HammerAnimation()
        {
            while (GameManager.GameIsStart == false)
            {
                yield return null;
            }

            while (true)
            {
                if (CheckDistance(_minDistance))
                {
                    SetAnimation("IsGameStart");
                    break;
                }

                yield return null;
            }

            while (true)
            {
                if (CheckDistance(_maxDistance))
                {
                    SetAnimation("IsStop");
                    break;
                }

                yield return null;
            }
        }

        private bool CheckDistance(float distance) => transform.position.z - _character.position.z < distance;
        private void SetAnimation(string animationName) => _animator.SetTrigger(animationName);
    }
}