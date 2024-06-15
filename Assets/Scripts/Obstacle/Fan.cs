using System.Collections;
using Manager;
using Manager.Audio.Utilities;
using ObjectPools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Obstacle
{
    public class Fan : MonoBehaviour
    {
        private Animator _animator;
        private BoxCollider _fanCollider;

        #region Awake, Start, Get Functions

        private void Awake()
        {
            GetReferences();
        }

        private void Start()
        {
            StartCoroutine(FanAnimation());
        }

        private void GetReferences()
        {
            _animator = GetComponentInChildren<Animator>();
            _fanCollider = GetComponent<BoxCollider>();
        }

        #endregion

        // Oyunun başlama durumuna göre random sürelerde fan'ın çalışmasını sağlar
        private IEnumerator FanAnimation()
        {
            bool gameIsStart;
            while (true)
            {
                gameIsStart = GameManager.GameIsStart;
                while (gameIsStart)
                {
                    _animator.SetBool("isStart", true);
                    _fanCollider.enabled = true;
                    FanFx.Instance.PlayFx();
                    yield return new WaitForSeconds(Random.Range(2f, 3.5f));
                    FxSounds.Instance.FanFx.Stop();
                    _animator.SetBool("isStart", false);
                    _fanCollider.enabled = false;
                    yield return new WaitForSeconds(Random.Range(1f, 2.8f));
                }

                yield return null;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (!other.CompareTag("Agent") &&
                (AgentPools.Instance.AgentCount != 1 || !other.CompareTag("Character"))) return;
            if (other.transform.position.x > transform.position.x)
                other.GetComponent<Rigidbody>().AddForce(new Vector3(3f, 0f, 0f), ForceMode.Impulse);
            else
                other.GetComponent<Rigidbody>().AddForce(new Vector3(-3f, 0f, 0f), ForceMode.Impulse);
        }
    }
}