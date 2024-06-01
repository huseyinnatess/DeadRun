using Controller.Utilities;
using ObjectPools;
using UnityEngine;

namespace Controller
{
    public class NeutralAgentController : MonoBehaviour
    {
        private SkinnedMeshRenderer _skinnedMeshRenderer; // Materyali, anakarakterin materyaline ayarlamak için
        private readonly Material[] _parentMaterial = new Material[1]; // Anakarakterin materyali için
        
        private Animator _animator;

        private Transform _parent;
        private GameObject _character;

        private Vector3 _characterColliderSize;

        #region Awake, Start, Get Functions

        private void Awake()
        {
            SetReferences();
        }

        private void SetReferences()
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
       
        // Agent veya Anakarakter ile çarpışırsa materyali anakarakterin materyaline ayarlanıp
        // koşma animasyonu tetikleniyor. Tag ve parenti diğer ajanlara göre ayarlanıp rotation ayarlanıyor.
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
        
        // Agent'ın materyal ve animasyonunu ayarlar
        private void SetAnimationMaterial()
        {
            _skinnedMeshRenderer.material = _parentMaterial[0];
            _animator.SetBool("Run", true);
        }
        
        // Agent'ın tagını ve parent'ını ayarlar
        private void SetTagAndParent()
        {
            gameObject.tag = "Agent";
            transform.parent = _parent;
        }

        // Rotation ayarlaması yapılıyor
        private void SetRotation()
        {
            var rotation = transform.rotation;
            rotation = Quaternion.Euler(rotation.x, 0f, rotation.z);
            transform.rotation = rotation;
        }
        
        // Agent'ın collider size'ını ayarlar
        private void SetColliderSize()
        {
            gameObject.GetComponent<BoxCollider>().size = _characterColliderSize;
        }
    }
}