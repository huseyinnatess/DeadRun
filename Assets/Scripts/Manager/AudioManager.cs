using UnityEngine;
using UnityEngine.UI;
using Utilities.SaveLoad;
using Utilities.UIElements;

namespace Manager
{
    public class AudioManager : MonoBehaviour
    {
        private Slider _soundSlider; // Ses slider'ı
        private Slider _musicSlider; // Müzik slider'ı

        private Slider[] _settingSliders; // Ayarlar panelindeki slider'ların tutulduğu array.
        
        #region Awake, Get Functions

        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            _settingSliders = SettingsPanel.Instance.SettingPanell.GetComponentsInChildren<Slider>();
            _soundSlider = _settingSliders[0];
            _musicSlider = _settingSliders[1];
            _soundSlider.value = GetSoundSliderValue();
            _musicSlider.value = GetMusicSliderValue();
        }

        #endregion

        /// <summary>
        /// Ses ayarını kayıt eder.
        /// </summary>
        /// <param name="slider">Kaydedilecek Slider</param>
        public void SetSoundSliderValue(Slider slider)
        {
            PlayerPrefsData.SetFloat("SoundSlider", slider.value);
            LevelPanelManager.Instance.UpdatePausePanelSliders();
        }
        
        /// <summary>
        /// Müzik ayarını kayıt eder.
        /// </summary>
        /// <param name="slider">Kaydedilecek Slider</param>
        public void SetMusicSliderValue(Slider slider)
        {
            PlayerPrefsData.SetFloat("MusicSlider", slider.value);
            LevelPanelManager.Instance.UpdatePausePanelSliders();
        }
        
        /// <summary>
        /// Kayıt edilmiş ses ayarını return eder.
        /// </summary>
        /// <returns></returns>
        public static float GetSoundSliderValue()
        {
            return PlayerPrefsData.GetFloat("SoundSlider");
        }

        /// <summary>
        /// Kayıt edilmiş müzik ayarını return eder.
        /// </summary>
        /// <returns></returns>
        public static float GetMusicSliderValue()
        {
            return PlayerPrefsData.GetFloat("MusicSlider");
        }
    }
}