using MonoSingleton;

namespace Manager.Audio.Abstract
{
    public abstract class ADistanceBasedAudioEffect : MonoSingleton<ADistanceBasedAudioEffect>
    {
        protected float Distance; // Anakarakter ve fan arasındaki mesafe.
        protected float MinDistance = 0.1f; // Sesin duyulacağı minimum mesafe.
        protected float MaxDistance = 4.0f; // Sesin tamamen maximum mesafe.
        
        /// <summary>
        /// Ses efektini oynatır.
        /// Bu metot türetilmiş sınıflar tarafından implement edilmelidir.
        /// </summary>
        public abstract void PlayFx();
        
        // Ses kaynağı ve karakter arasındaki mesafeyi hesaplar.
        protected abstract void CalculateDistance();
    }
}
