using Manager.Audio.Utilities;
using MonoSingleton;
using UnityEngine;
using UnityEngine.UI;

namespace Manager.Audio
{
    public class ButtonSoundsManager : MonoSingleton<ButtonSoundsManager>
    {
        private Button[] _buttons; // Sahnedeki tüm butonların tutulduğu dizi.
        private AudioSource _audioSource; // Butonlara eklenecek ses kaynağı.

        #region Start

        private void Start()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.clip = FxSounds.Instance.ButtonClickFx;
            SetButtonFxVolume();
            GetAllButtonsInScene();
        }

        #endregion
        
        /// <summary>
        /// Buton efektinin sesini Fx Slider'a göre ayarlar.
        /// </summary>
        public void SetButtonFxVolume()
        {
            _audioSource.volume = AudioManager.GetFxSliderValue();
        }
        
        // Sahnedeki aktif ve pasif tüm butonları bulur.
        private void GetAllButtonsInScene()
        {
            _buttons = Resources.FindObjectsOfTypeAll<Button>();
            for (int i = 0; i < _buttons.Length; i++)
            {
                _buttons[i].onClick.AddListener(() => PlayButtonClickSound());
            }
        }
        
        // Buton click sesinin çalışmasını sağlar.
        private void PlayButtonClickSound()
        {
            _audioSource.Play();
        }
    }
}