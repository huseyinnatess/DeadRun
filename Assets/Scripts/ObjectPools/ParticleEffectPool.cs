using System.Collections.Generic;
using System.Linq;
using MonoSingleton;
using UnityEngine;

namespace ObjectPools
{
    public class ParticleEffectPool : MonoSingleton<ParticleEffectPool>
    {
        public List<ParticleSystem> SpawnEffects; // Spawn efekti.
        public List<ParticleSystem> DeadEffects; // Ölüm efekti.
        public List<ParticleSystem> ConfettiEffects; // Kutlama efekti.

        

        /// <summary>
        /// Spawn efekti oluşturur
        /// </summary>
        /// <param name="create">Efektin oluşacağı yer</param>
        public void SpawnEffectPool(Transform create)
        {
            foreach (var item in SpawnEffects.Where(item => !item.gameObject.activeInHierarchy))
            {
                item.transform.position = new Vector3(create.position.x, 1f, create.position.z);
                item.gameObject.SetActive(true);
                break;
            }
        }
        /// <summary>
        /// Ölüm efekti oluşturur
        /// </summary>
        /// <param name="create">Efektin oluşacağı yer</param>
        public void DeadEffectPool(Transform create)
        {
            foreach (var item in DeadEffects.Where(item => !item.gameObject.activeInHierarchy))
            {
                item.transform.position = new Vector3(create.position.x, 1f, create.position.z);
                item.gameObject.SetActive(true);
                break;
            }
        }
        /// <summary>
        /// Kutlama efekti oluşturur
        /// </summary>
        /// <param name="create">Efektin oluşacağı yer</param>
        public void ConfettiEffectPool(Transform create)
        {
            foreach (var item in ConfettiEffects.Where(item => !item.gameObject.activeInHierarchy))
            {
                item.transform.position = new Vector3(create.position.x, create.position.y + .5f, create.position.z);
                item.gameObject.SetActive(true);
            }
        }
    
    }
}
