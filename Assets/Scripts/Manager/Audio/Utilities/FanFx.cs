using Manager.Audio.Abstract;
using UnityEngine;

namespace Manager.Audio.Utilities
{
    public class FanFx : ADistanceBasedAudioEffect
    {
        private FxSounds _fxSounds;

        private Transform _character;

        #region Awake

        private void Awake()
        {
            _character = GameObject.FindWithTag("Character").transform;
            _fxSounds = FxSounds.Instance;
        }

        #endregion


        public override void PlayFx()
        {
            CalculateDistance();
            if (AudioManager.GetFxSliderValue() != 0f)
                _fxSounds.SetVolumeToDistance(_fxSounds.FanFx, Distance - .8f, MinDistance, MaxDistance - MinDistance);
            if (_fxSounds.FanFx.isPlaying)
                _fxSounds.FanFx.Play();
        }

        protected override void CalculateDistance()
        {
            Distance = Vector3.Distance(_character.position, transform.position);
        }
    }
}