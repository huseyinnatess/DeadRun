using MonoSingleton;
using UnityEngine;

namespace Manager.Audio.Utilities
{
    public class GameSounds : MonoSingleton<GameSounds>
    {
        [Header("GameMusic")] 
        public AudioSource _gameMusic; // Oyunun anamüziği

        #region Start

        private void Start()
        {
            SetGameSoundVolume();
        }

        #endregion
        
        
        /// <summary>
        /// Anamüziğin sesini sound slider'a göre ayarlar.
        /// </summary>
        public void SetGameSoundVolume()
        {
            _gameMusic.volume = AudioManager.GetSoundSliderValue();
        }
    }
}