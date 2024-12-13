using MonoSingleton;

namespace Manager.Audio.Abstract
{
    public abstract class ADistanceBasedAudioEffect : MonoSingleton<ADistanceBasedAudioEffect>
    {
        protected float Distance;
        protected float MinDistance = 0.1f;
        protected float MaxDistance = 4.0f;


        public abstract void PlayFx();
        protected abstract void CalculateDistance();
    }
}