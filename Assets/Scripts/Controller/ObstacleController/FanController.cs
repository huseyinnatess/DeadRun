using System.Collections;
using Manager;
using Manager.Audio.Utilities;
using ObjectPools;
using UnityEngine;

namespace Controller.ObstacleController
{
    public class FanController : MonoBehaviour
    {
        private Animator _animator;
        private BoxCollider _fanCollider;
        
        private bool _isFanRight;
        private Vector3 _fanForce;
        
        #region Awake, Start, Get, Set
        
        private void Awake()
        {
            GetReferences();
            SetReferences();
        }
        private void GetReferences()
        {
            _animator = GetComponentInChildren<Animator>();
            _fanCollider = GetComponent<BoxCollider>();
        }

        private void SetReferences()
        {
            _isFanRight = gameObject.CompareTag("FanRight");
            _fanForce = new Vector3(2.8f, 0f, 0f);
        }

        private void Start()
        {
            StartCoroutine(FanAnimation());
        }
        
        #endregion
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
            if (_isFanRight)
                other.GetComponent<Rigidbody>().AddForce(-_fanForce, ForceMode.Impulse);
            else
                other.GetComponent<Rigidbody>().AddForce(_fanForce, ForceMode.Impulse);
        }
    }
}