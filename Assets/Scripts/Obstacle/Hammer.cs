using System;
using System.Collections;
using Manager;
using UnityEngine;

namespace Obstacle
{
    public class Hammer : MonoBehaviour
    {
        private Animator _animator; // Hammer'ın animatörü.
        private Transform _character;

        private float _minDistance;
        private float _maxDistance;
        
        #region Start

        private void Awake()
        {
            _minDistance = 4f;
            _maxDistance = -2f;
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _character = GameObject.FindWithTag("Character").transform;
            StartCoroutine(HammerAnimation());
        }
        #endregion
        
        // Oyunun başlama durumuna göre hammer animasyonunu başlatır.
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
        
        // Karakter ile nesne arasındaki mesafe kontrolünü yapar.
        private bool CheckDistance(float distance) =>  transform.position.z - _character.position.z < distance;
        
        // Parametre olarak gelen animasyonu çalıştırır.
        private void SetAnimation(string animationName) => _animator.SetTrigger(animationName);
    }
}