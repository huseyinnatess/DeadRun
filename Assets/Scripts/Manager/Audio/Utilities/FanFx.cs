using Manager.Audio.Abstract;
using UnityEngine;

namespace Manager.Audio.Utilities
{
    public class FanFx : ADistanceBasedAudioEffect
    {
        private FxSounds _fxSounds;
        
        private Transform _character; // Anakarakter.

        #region Awake

        private void Awake()
        {
            _character = GameObject.FindWithTag("Character").transform;
            _fxSounds = FxSounds.Instance;
        }
        #endregion
        
        /// <summary>
        /// Fan efekt sesini oynatır.
        /// </summary>
        public override void PlayFx()
        {
            CalculateDistance();
            if (AudioManager.GetFxSliderValue() != 0f)
                _fxSounds.SetVolumeToDistance(_fxSounds.FanFx, Distance, MinDistance, MaxDistance - MinDistance);
            _fxSounds.FanFx.Play();
        }
        
        
        // Ses kaynağı ve karakter arasındaki mesafeyi hesaplar.
        protected override void CalculateDistance()
        {
            Distance = Vector3.Distance(_character.position, transform.position);

        }
    }
}