using System.Collections;
using Manager;
using UnityEngine;

namespace Obstacle
{
    public class Hammer : MonoBehaviour
    {
        private Animator _animator; // Hammer'ın animatörü.

        #region Awake
        private void Start()
        {
            _animator = GetComponent<Animator>();
            StartCoroutine(HammerAnimation());
        }
        #endregion
        
        // Oyunun başlama durumuna göre hammer animasyonunu başlatıp dosyayı yok eder.
        private IEnumerator HammerAnimation()
        {
            while (GameManager.GameIsStart == false)
            {
                yield return null;
            }
            _animator.SetTrigger("IsGameStart");
        }
    }
}