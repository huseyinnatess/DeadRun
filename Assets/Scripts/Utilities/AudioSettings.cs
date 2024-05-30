using UnityEngine;
using UnityEngine.UI;
using Utilities.SaveLoad;

namespace Utilities
{
    public class AudioSettings : MonoBehaviour
    {
        private Slider _soundSlider;
        private Slider _fxSlider;

        #region Awake, Get Functions

        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            _soundSlider = GameObject.FindWithTag("SoundSlider").GetComponent<Slider>();
            _fxSlider = GameObject.FindWithTag("FxSlider").GetComponent<Slider>();
            _soundSlider.value = GetSoundSliderValue();
            _fxSlider.value = GetFxSliderValue();
        }

        #endregion


        public void SetSoundSliderValue()
        {
            PlayerPrefsData.SetFloat("SoundSlider", _soundSlider.value);
        }

        public void SetFxSliderValue()
        {
            PlayerPrefsData.SetFloat("FxSlider", _fxSlider.value);
        }

        public static float GetSoundSliderValue()
        {
            return PlayerPrefsData.GetFloat("SoundSlider");
        }

        public static float GetFxSliderValue()
        {
            return PlayerPrefsData.GetFloat("FxSlider");
        }
    }
}