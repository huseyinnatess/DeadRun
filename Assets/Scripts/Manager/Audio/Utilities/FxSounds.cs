using System.Collections;
using MonoSingleton;
using UnityEngine;

namespace Manager.Audio.Utilities
{
    public class FxSounds : MonoSingleton<FxSounds>
    {
        public AudioClip ButtonClickFx;
        [Header("Agents Fx")] 
        public AudioSource SpawnAgentFx;
        public AudioClip SpawnAgentClip;
        public AudioSource DeadAgentFx;
        public AudioClip DeadAgentClip;
        [Header("Game Panels Fx")] 
        public AudioSource VictoryFx;
        public AudioSource DefeatFx;
        [Header("Character Fx")] 
        public AudioSource MoveSoundFx;
        public AudioSource CharacterRunFx;
        [Header("Obstacles")] 
        public AudioSource HammerFx;
        public AudioSource FanFx;

        #region Start

        private void Start()
        {
            SetFxSoundsVolume();
        }

        #endregion


        public void SetFxSoundsVolume()
        {
            float fxSliderValue = AudioManager.GetFxSliderValue();
            SpawnAgentFx.volume = fxSliderValue;
            DeadAgentFx.volume = fxSliderValue;
            VictoryFx.volume = fxSliderValue;
            DefeatFx.volume = fxSliderValue;
            MoveSoundFx.volume = fxSliderValue;
            CharacterRunFx.volume = fxSliderValue;
            HammerFx.volume = fxSliderValue - .2f;
            FanFx.volume = fxSliderValue;
        }


        public IEnumerator RepeatedSound(AudioSource audioSource, AudioClip audioClip = default, int repeat = 1)
        {
            repeat = repeat < 0 ? repeat * -1 : repeat;
            while (repeat != 0)
            {
                audioSource.Play();
                repeat--;
                yield return new WaitForSeconds(.1f);
            }
        }


        public void SetVolumeToDistance(AudioSource audioManager, float distance, float minDistance,
            float distanceRange)
        {
            audioManager.volume = 1.0f - (distance - minDistance) / distanceRange;
        }
    }
}