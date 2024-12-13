using System;
using System.Collections;
using Manager;
using UnityEngine;

namespace Controller.ObstacleController
{
    public class SawController : MonoBehaviour
    {
        private Animator _animator;
        private void Awake()
        {
            GetReferences();
        }

        private void Start()
        {
            StartCoroutine(SawAnimation());
        }

        private void GetReferences()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        private IEnumerator SawAnimation()
        {
            while (GameManager.GameIsStart == false)
            {
                yield return null;
            }
            _animator.enabled = true;
        }
    }
}