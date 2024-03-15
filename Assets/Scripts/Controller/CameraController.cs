using UnityEngine;

namespace Controller
{
    public class CameraController : MonoBehaviour
    {
        private Transform _target;
        private Vector3 _targetOffset;
        private Vector3 _finishPosition;
        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            _target = GameObject.FindWithTag("Character").gameObject.transform;
            SetReferences();
        }
        private void SetReferences()
        {
            _targetOffset = transform.position - _target.position;
        }

        private void LateUpdate()
        {
            SetCameraPosition();
        }

        private void SetCameraPosition()
        {
            if (!EnemyController.IsCanAttack)
            {
                var position = transform.position;
                transform.position = Vector3.Lerp(position, _target.position + _targetOffset, .125f);
                _finishPosition = new Vector3(position.x, position.y + 1f, position.z - 1.5f);
            }
            else
                transform.position = Vector3.Lerp(transform.position, _finishPosition, .100f);
        }
    }
}
