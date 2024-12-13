using UnityEngine;
using UnityEngine.UI;

namespace Utilities.UIElements
{
    public class RunSlider : MonoBehaviour
    {
        private Slider _runSlider;
        private float _runSliderMaxValue;
        private Transform _finishPosition;
        private Transform _characterPosition;

        #region Awake, Start, Get, Set Functions

        private void Awake()
        {
            _runSlider = GetComponent<Slider>();
        }

        private void Start()
        {
            GetReferences();
            SetReferences();
        }

        private void GetReferences()
        {
            _finishPosition = GameObject.FindWithTag("Battlefield").transform;
            _characterPosition = GameObject.FindWithTag("Character").transform;
        }

        private void SetReferences()
        {
            _runSlider.maxValue = Vector3.Distance(_finishPosition.position, _characterPosition.position);
            _runSliderMaxValue = _runSlider.maxValue;
        }

        #endregion

        #region LateUpdate

        private void LateUpdate()
        {
            SliderUpdate();
        }

        #endregion


        private void SliderUpdate()
        {
            if (Mathf.Approximately(_runSlider.value, _runSliderMaxValue)) return;
            _runSlider.value = _runSliderMaxValue -
                Vector3.Distance(_characterPosition.position, _finishPosition.position) + 0.58f;
        }
    }
}