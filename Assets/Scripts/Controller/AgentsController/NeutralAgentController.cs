using ObjectPools;
using UnityEngine;

namespace Controller.AgentsController
{
    public class NeutralAgentController : MonoBehaviour
    {
        private SkinnedMeshRenderer _skinnedMeshRenderer;
        private readonly Material[] _parentMaterial = new Material[1];
        private Animator _animator;
        private Transform _parent;
        private GameObject _character;
        private Vector3 _characterColliderSize;

        #region Awake, Start, Get Functions

        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
            _animator = GetComponent<Animator>();
            _parent = GameObject.FindWithTag("AgentPool").transform;
            _character = GameObject.FindWithTag("Character");
            _characterColliderSize = _character.GetComponent<BoxCollider>().size;
        }

        private void Start()
        {
            _parentMaterial[0] = _character.GetComponentInChildren<SkinnedMeshRenderer>().material;
        }

        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Agent") && !other.CompareTag("Character")) return;
            SetAnimationMaterial();
            SetTagAndParent();
            AgentController.Instance.UpdateAgentsComponent();
            SetRotation();
            AgentPools.Instance.AddList(gameObject);
            SetColliderSize();
            Destroy(this);
        }
        private void SetAnimationMaterial()
        {
            _skinnedMeshRenderer.material = _parentMaterial[0];
            _animator.SetBool("Run", true);
        }
        private void SetTagAndParent()
        {
            gameObject.tag = "Agent";
            transform.parent = _parent;
        }
        private void SetRotation()
        {
            var rotation = transform.rotation;
            rotation = Quaternion.Euler(rotation.x, 0f, rotation.z);
            transform.rotation = rotation;
        }
        private void SetColliderSize()
        {
            gameObject.GetComponent<BoxCollider>().size = _characterColliderSize;
        }
    }
}