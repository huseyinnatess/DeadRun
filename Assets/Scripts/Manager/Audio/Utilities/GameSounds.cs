using MonoSingleton;
using UnityEngine;

namespace Manager.Audio.Utilities
{
    public class GameSounds : MonoSingleton<GameSounds>
    {
        [Header("GameMusic")] public AudioSource GameMusic;

        #region Start

        private void Start()
        {
            SetGameSoundVolume();
        }

        #endregion


        public void SetGameSoundVolume()
        {
            GameMusic.volume = AudioManager.GetSoundSliderValue();
        }
    }
}