using System.Collections.Generic;
using UnityEngine;

namespace ObjectPools
{
    public class ParticleEffectPool : MonoBehaviour
    {
        public List<ParticleSystem> spawnEffects;
        public List<ParticleSystem> deadEffects;
        public static ParticleEffectPool Instance;

        private void Awake()
        {
            Singleton();
        }

        private void Singleton()
        {
            if (!Instance)
                Instance = this;
        }

        public void SpawnEffectPool(Transform create)
        {
            foreach (var item in spawnEffects)
            {
                if (!item.gameObject.activeInHierarchy)
                {
                    item.transform.position = new Vector3(create.position.x, 1f, create.position.z);
                    item.gameObject.SetActive(true);
                    break;
                }
            }
        }
    
        public void DeadEffectPool(Transform create)
        {
            foreach (var item in deadEffects)
            {
                if (!item.gameObject.activeInHierarchy)
                {
                    item.transform.position = new Vector3(create.position.x, 1f, create.position.z);
                    item.gameObject.SetActive(true);
                    break;
                }
            }
        }
    
    }
}
