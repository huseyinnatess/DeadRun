using System.Collections.Generic;
using MonoSingleton;
using UnityEngine;

namespace ObjectPools
{
    public class ParticleEffectPool : MonoSingleton<ParticleEffectPool>
    {
        public List<ParticleSystem> spawnEffects;
        public List<ParticleSystem> deadEffects;
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
