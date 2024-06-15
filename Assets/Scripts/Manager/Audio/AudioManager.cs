using Manager.Audio.Utilities;
using UnityEngine;
using UnityEngine.UI;
using Utilities.SaveLoad;

namespace Manager.Audio
{
    public class AudioManager : MonoBehaviour
    {
        /// <summary>
        /// Ses ayarını kayıt eder.
        /// </summary>
        /// <param name="slider">Kaydedilecek Slider</param>
        public void SetSoundSliderValue(Slider slider)
        {
            PlayerPrefsData.SetFloat("SoundSlider", slider.value);
            GameSounds.Instance.SetGameSoundVolume();
            LevelPanelManager.Instance.UpdatePausePanelSliders();
        }
        
        /// <summary>
        /// Müzik ayarını kayıt eder.
        /// </summary>
        /// <param name="slider">Kaydedilecek Slider</param>
        public void SetFxSliderValue(Slider slider)
        {
            PlayerPrefsData.SetFloat("FxSlider", slider.value);
            ButtonSoundsManager.Instance.SetButtonFxVolume();
            FxSounds.Instance.SetFxSoundsVolume();
            LevelPanelManager.Instance.UpdatePausePanelSliders();
        }
        
        /// <summary>
        /// Kayıt edilmiş ses ayarını return eder.
        /// </summary>
        /// <returns>Sound Slider'ın anlık value değeri.</returns>
        public static float GetSoundSliderValue()
        {
            return PlayerPrefsData.GetFloat("SoundSlider");
        }

        /// <summary>
        /// Kayıt edilmiş müzik ayarını return eder.
        /// </summary>
        /// <returns>Fx Slider'ın anlık value değeri.</returns>
        public static float GetFxSliderValue()
        {
            return PlayerPrefsData.GetFloat("FxSlider");
        }
    }
}