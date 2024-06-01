using ObjectPools;
using UnityEngine;

namespace Controller
{
    public class NeutralAgentController : MonoBehaviour
    {
        private SkinnedMeshRenderer _skinnedMeshRenderer; // Materyali, anakarakterin materyaline ayarlamak için
        private Animator _animator;
        private readonly Material[] _parentMaterial = new Material[1]; // Anakarakterin materyali için

        private Transform _parent;
        private static readonly int Run = Animator.StringToHash("Run");

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
        }

        private void Start()
        {
            _parentMaterial[0] = GameObject.FindWithTag("Character").GetComponentInChildren<SkinnedMeshRenderer>().material;
        }

        #endregion
       
        // Agent veya Anakarakter ile çarpışırsa materyali anakarakterin materyaline ayarlanıp
        // koşma animasyonu tetikleniyor. Tag ve parenti diğer ajanlara göre ayarlanıp rotation ayarlanıyor.
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Agent") && !other.CompareTag("Character")) return;
            _skinnedMeshRenderer.material = _parentMaterial[0];
            _animator.SetBool(Run, true);
            gameObject.tag = "Agent";
            transform.parent = _parent;
            AgentController.Instance.UpdateAgentsComponent();
            SetRotation();
            AgentPools.Instance.AddList(gameObject);
            Destroy(this);
        }
        
        // Rotation ayarlaması yapılıyor
        private void SetRotation()
        {
            var rotation = transform.rotation;
            rotation = Quaternion.Euler(rotation.x, 0f, rotation.z);
            transform.rotation = rotation;
        }
    }
}