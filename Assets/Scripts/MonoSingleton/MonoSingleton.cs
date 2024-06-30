using UnityEngine;

namespace MonoSingleton
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        
        private static volatile T _instance = null;

        public static T Instance => _instance ? _instance : FindFirstObjectByType(typeof(T)) as T;
    }
}