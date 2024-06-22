using Manager.Audio.Utilities;
using MonoSingleton;
using UnityEngine.UI;
using Utilities.SaveLoad;

namespace Manager.Audio
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        private static bool _instanceExists = false; // Bu sınıfın bir örneğinin zaten var olup olmadığını kontrol eder.

        #region Awake

        private void Awake()
        {
            CheckInstance();
        }

        #endregion
       
        //  Sahnede bu oyun nesnesinin birden fazla örneğinin oluşmasını engeller.
        private void CheckInstance()
        {
            if (_instanceExists)
            {
                Destroy(gameObject);
            }
            else
            {
                _instanceExists = true;
                DontDestroyOnLoad(gameObject);
            }
        }

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