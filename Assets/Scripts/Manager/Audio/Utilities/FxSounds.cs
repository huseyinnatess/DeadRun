using System.Collections;
using MonoSingleton;
using UnityEngine;

namespace Manager.Audio.Utilities
{
    public class FxSounds : MonoSingleton<FxSounds>
    {
        public AudioClip ButtonClickFx; // Buton tıklama efekti.
        
        public AudioClip SpawnAgentClip;
        public AudioSource SpawnAgentFx; // Agent'lar spawn efekti.
        
        public AudioSource VictoryFx; // Victory efekti.
        public AudioSource DefeatFx; // Defeat efekti.
        
        public AudioSource MoveSoundFx; // Karakterin sağa sola hareket sesi.

        public AudioSource HammerFx;

        private bool _isStartCoroutine;
        
        #region Start
        private void Start()
        {
            _isStartCoroutine = false;
            SetFxSoundsVolume();
        }
        
        #endregion
        
        /// <summary>
        /// Fx Sound'ların volume değerlerini ayarlar.
        /// </summary>
        public void SetFxSoundsVolume()
        {
            float fxSliderValue = AudioManager.GetFxSliderValue();
            SpawnAgentFx.volume = fxSliderValue;
            VictoryFx.volume = fxSliderValue;
            MoveSoundFx.volume = fxSliderValue;
            DefeatFx.volume = fxSliderValue;
            HammerFx.volume = fxSliderValue;
        }
        
        /// <summary>
        /// Bir ses'i tekrarlı şekilde oynatır.
        /// </summary>
        /// <param name="audioSource">Oynatılacak ses kaynağı.</param>
        /// <param name="repeat">Sesin oynatılacağı tekrar sayısı.</param>
        /// <returns>Herhangi bir return değeri yok.</returns>
        public IEnumerator RepeatedSound(AudioSource audioSource, int repeat)
        {
            _isStartCoroutine = true;
            while (repeat != 0 && _isStartCoroutine)
            {
                audioSource.Play();
                repeat--;
                yield return new WaitForSeconds(.1f);
            }
            if (repeat == 0)
                _isStartCoroutine = true;
        }
    }
}