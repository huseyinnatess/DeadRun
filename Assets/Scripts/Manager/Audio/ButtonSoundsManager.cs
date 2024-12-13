using Manager.Audio.Utilities;
using MonoSingleton;
using UnityEngine;
using UnityEngine.UI;

namespace Manager.Audio
{
    public class ButtonSoundsManager : MonoSingleton<ButtonSoundsManager>
    {
        private Button[] _buttons;
        private AudioSource _audioSource;

        #region Awake, Start

        private void Awake()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.playOnAwake = false;
        }

        private void Start()
        {
            _audioSource.clip = FxSounds.Instance.ButtonClickFx;
            SetButtonFxVolume();
            GetAllButtonsInScene();
        }

        #endregion


        public void SetButtonFxVolume()
        {
            _audioSource.volume = AudioManager.GetFxSliderValue();
        }

        private void GetAllButtonsInScene()
        {
            _buttons = Resources.FindObjectsOfTypeAll<Button>();
            for (int i = 0; i < _buttons.Length; i++)
            {
                _buttons[i].onClick.AddListener(() => PlayButtonClickSound());
            }
        }

        private void PlayButtonClickSound()
        {
            _audioSource.Play();
        }
    }
}