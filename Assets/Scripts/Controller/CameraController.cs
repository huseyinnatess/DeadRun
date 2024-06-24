using UnityEngine;

namespace Controller
{
    public class CameraController : MonoBehaviour
    {
        private Transform _target; // Kameranın takip edeceği hedef
        private Vector3 _targetOffset; // Kamera ve target arasındaki takip mesafesi
        private Vector3 _finishPosition; // Bitiş çizgisi geçildikten sonraki kamera pozisyonu

        #region Start, Get, Set Functions

        private void Start()
        {
            GetReferences();
            SetReferences();
        }

        private void GetReferences()
        {
            _target = CharacterControl.Instance.transform;
        }
        private void SetReferences()
        {
            _targetOffset = transform.position - _target.position;
        }

        #endregion

        #region LateUpdate, SetCameraPosition Functions

        private void LateUpdate()
        { 
            SetCameraPosition();
        }
        
        // LateUpdate
        // Target veya finish position'a göre oyun içerisinde kamera konumunu güncelliyor
        private void SetCameraPosition()
        {
            if (!EnemyController.IsCanAttack)
            {
                Vector3 position = transform.position;
                transform.position = Vector3.Lerp(position, _target.position + _targetOffset, .125f);
                _finishPosition = new Vector3(position.x, position.y + 1f, position.z - 1.5f);
            }
            else
                transform.position = Vector3.Lerp(transform.position, _finishPosition, .100f);
        }

        #endregion
       
    }
}
