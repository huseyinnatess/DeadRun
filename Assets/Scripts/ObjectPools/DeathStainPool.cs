using MonoSingleton;
using UnityEngine;

namespace ObjectPools
{
    public class DeathStainPool : MonoSingleton<DeathStainPool>
    {
        public GameObject[] deathStains; // Ölüm lekelerinin listesi
        
        /// <summary>
        /// Ölüm lekeleri oluşturur veya pasif eder
        /// </summary>
        /// <param name="setActive"> Ölüm lekesinin aktifliğini belirler</param>
        /// <param name="create"> Ölüm lekesinin oluşma yeri </param>
        public void DeathStainObjectPool(bool setActive, Transform create)
        {
            for (int i = 0; i < deathStains.Length; i++)
            {
                if (!deathStains[i].activeInHierarchy && setActive)
                {
                    deathStains[i].transform.position = new Vector3(create.position.x, 1.05f, create.position.z);
                    deathStains[i].SetActive(true);
                    break;
                }
                if (deathStains[i].activeInHierarchy && !setActive)
                {
                    deathStains[i].SetActive(false);
                    break;
                }
            }
        }
    }
}
