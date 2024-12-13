using Manager.Audio.Utilities;
using MonoSingleton;
using UnityEngine.UI;
using Utilities.SaveLoad;

namespace Manager.Audio
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        private GameSounds _gameSounds;
        private FxSounds _fxSounds;
        private LevelPanelManager _levelPanelManager;
        private ButtonSoundsManager _buttonSoundsManager;
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


        public void SetSoundSliderValue(Slider slider)
        {
            PlayerPrefsData.SetFloat("SoundSlider", slider.value);
            _gameSounds.SetGameSoundVolume();
            _levelPanelManager.UpdatePausePanelSliders();
        }


        public void SetFxSliderValue(Slider slider)
        {
            PlayerPrefsData.SetFloat("FxSlider", slider.value);
            _buttonSoundsManager.SetButtonFxVolume();
            _fxSounds.SetFxSoundsVolume();
            _levelPanelManager.UpdatePausePanelSliders();
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