using Manager.Audio.Utilities;
using MonoSingleton;
using UnityEngine.UI;
using Utilities.SaveLoad;

namespace Manager.Audio
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        private GameSounds _gameSounds; // Oyunun ana sesini tutan script.
        private FxSounds _fxSounds; // Oyunun efekt seslerini tutan script.
        private LevelPanelManager _levelPanelManager; // LevelPanelManager scripti.
        private ButtonSoundsManager _buttonSoundsManager; // ButtonSoundsManager scripti.

        #region Start
        private void Start()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            _gameSounds = GameSounds.Instance;
            _fxSounds = FxSounds.Instance;
            _levelPanelManager = LevelPanelManager.Instance;
            _buttonSoundsManager = ButtonSoundsManager.Instance;
        }
        #endregion


        /// <summary>
        /// Ses ayarını kayıt eder.
        /// </summary>
        /// <param name="slider">Kaydedilecek Slider</param>
        public void SetSoundSliderValue(Slider slider)
        {
            PlayerPrefsData.SetFloat("SoundSlider", slider.value);
            _gameSounds.SetGameSoundVolume();
            _levelPanelManager.UpdatePausePanelSliders();
        }

        /// <summary>
        /// Müzik ayarını kayıt eder.
        /// </summary>
        /// <param name="slider">Kaydedilecek Slider</param>
        public void SetFxSliderValue(Slider slider)
        {
            PlayerPrefsData.SetFloat("FxSlider", slider.value);
            _buttonSoundsManager.SetButtonFxVolume();
            _fxSounds.SetFxSoundsVolume();
            _levelPanelManager.UpdatePausePanelSliders();
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