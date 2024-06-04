using UnityEngine;
using UnityEngine.UI;
using Utilities.SaveLoad;

namespace Utilities
{
    public class AudioSettings : MonoBehaviour
    {
        private Slider _soundSlider;
        private Slider _musicSlider;

        #region Awake, Get Functions

        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            _soundSlider = GameObject.FindWithTag("SoundSlider").GetComponent<Slider>();
            _musicSlider = GameObject.FindWithTag("MusicSlider").GetComponent<Slider>();
            _soundSlider.value = GetSoundSliderValue();
            _musicSlider.value = GetMusicSliderValue();
        }

        #endregion


        public void SetSoundSliderValue(Slider slider)
        {
            PlayerPrefsData.SetFloat("SoundSlider", slider.value);
        }

        public void SetMusicSliderValue(Slider slider)
        {
            PlayerPrefsData.SetFloat("MusicSlider", slider.value);
        }

        public static float GetSoundSliderValue()
        {
            return PlayerPrefsData.GetFloat("SoundSlider");
        }

        public static float GetMusicSliderValue()
        {
            return PlayerPrefsData.GetFloat("MusicSlider");
        }
    }
}