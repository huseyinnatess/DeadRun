using UnityEngine;

namespace Manager.Audio.Utilities
{
    public class HammerFx : MonoBehaviour
    {
        private float _minDistance = 0.1f; // En yüksek volumde duyulacak mesafe
        private float _maxDistance = 4.0f; // Sesin tamamen duyulmaz hale geleceği mesafe
        private float _distanceRange; // Maximum ve minimum distance'ın farkı.
        
        private Transform _character; // Anakarakter

        private float _distance; // Anakarakter ve hammer arasındaki mesafe.

        #region Awake
        private void Awake()
        {
            _character = GameObject.FindWithTag("Character").transform;
            _distanceRange = _maxDistance - _minDistance;
        }
        #endregion
        
        /// <summary>
        /// Hammer vuruş efektini oynatır.
        /// </summary>
        public void PlayHammerFx()
        {
            CalculateDistance();
            SetHammerFxVolume();
            FxSounds.Instance.HammerFx.Play();
        }
        
        // Hammer efektinin volume değerini ayarlar.
        private void SetHammerFxVolume()
        {
            if (AudioManager.GetFxSliderValue() != 0f)
                FxSounds.Instance.HammerFx.volume = 1.0f - (_distance - _minDistance) / _distanceRange;
        }
        
        // Hammer ve karakter arasındaki mesafeyi hesaplar.
        private void CalculateDistance()
        {
            _distance = Vector3.Distance(_character.position, transform.position);
        }
    }
}