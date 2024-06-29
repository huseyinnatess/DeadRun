using Manager.Audio.Abstract;
using UnityEngine;

namespace Manager.Audio.Utilities
{
    public class HammerFx : ADistanceBasedAudioEffect
    {
        private Transform _character; // Anakarakter

        private FxSounds _fxSounds;

        #region Awake, Start

        private void Awake()
        {
            _fxSounds = FxSounds.Instance;
        }

        private void Start()
        {
            _character = GameObject.FindWithTag("Character").transform;
        }

        #endregion

        /// <summary>
        /// Hammer vuruş efekt sesini oynatır.
        /// </summary>
        public override void PlayFx()
        {
            CalculateDistance();
            if (AudioManager.GetFxSliderValue() != 0f)
                _fxSounds.SetVolumeToDistance(_fxSounds.HammerFx, Distance - .8f, MinDistance,
                    MaxDistance - MinDistance);
            _fxSounds.HammerFx.Play();
        }

        // Hammer ve karakter arasındaki mesafeyi hesaplar.
        protected override void CalculateDistance()
        {
            Distance = Vector3.Distance(_character.position, transform.position);
        }
    }
}