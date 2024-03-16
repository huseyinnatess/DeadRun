using System;
using UnityEngine;

namespace MonoSingleton
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static volatile T instance = null;
        
        public static T Instance => instance ? instance : (instance = FindObjectOfType(typeof(T)) as T);
    }
}