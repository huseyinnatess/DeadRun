using System.Collections;
using ObjectPools;
using UnityEngine;

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


        private IEnumerator FanAnimation()
        {
            while (true)
            {
                _animator.SetBool("isStart", true);
                _fanCollider.enabled = true;
                yield return new WaitForSeconds(Random.Range(2f, 3.5f));
                _animator.SetBool("isStart", false);
                _fanCollider.enabled = false;
                yield return new WaitForSeconds(Random.Range(1f, 2.8f));
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Agent") || (AgentPools.Instance.AgentCount == 1 && other.CompareTag("Character")))
            {
                if (other.transform.position.x > transform.position.x)
                    other.GetComponent<Rigidbody>().AddForce(new Vector3(3f, 0f, 0f), ForceMode.Impulse);
                else
                    other.GetComponent<Rigidbody>().AddForce(new Vector3(-3f, 0f, 0f), ForceMode.Impulse);
            }
        }
    }
}