using MonoSingleton;
using UnityEngine;

namespace ObjectPools
{
    public class DeathStainPool : MonoSingleton<DeathStainPool>
    {
        public GameObject[] deathStains;


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