using System;
using Controller;
using ObjectPools;
using UnityEngine;

namespace Utilities
{
    public class NeutralAgent : MonoBehaviour
    {
        private SkinnedMeshRenderer _skinnedMeshRenderer;
        private Animator _animator;
        private readonly Material[] _parentMaterial = new Material[1];
        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
             _parentMaterial[0] = GameObject.FindWithTag("Character").GetComponentInChildren<SkinnedMeshRenderer>().material;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Agent") && !other.CompareTag("Character")) return;
            _skinnedMeshRenderer.material = _parentMaterial[0];
            _animator.SetBool("Run", true);
            gameObject.tag = "Agent";
            gameObject.AddComponent<AgentController>();
            SetRotation();
            AgentPools.Instance.AddList(gameObject);
            Destroy(this);
        }

        private void SetRotation()
        {
            var rotation = transform.rotation;
            rotation = Quaternion.Euler(rotation.x, 0f, rotation.z);
            transform.rotation = rotation;
        }
    }
}