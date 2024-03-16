using System.Collections.Generic;
using MonoSingleton;
using UnityEngine;

namespace ObjectPools
{
    public class DeathStainPool : MonoSingleton<DeathStainPool>
    {
        public GameObject[] deathStains;

        public void DeathStainObjectPool(bool setActive, Transform create)
        {
            foreach (var item in deathStains)
            {
                if (!item.activeInHierarchy && setActive)
                {
                    item.transform.position = new Vector3(create.position.x, 1.05f, create.position.z);
                    item.SetActive(true);
                    break;
                }

                if (item.activeInHierarchy && !setActive)
                {
                    item.SetActive(false);
                    break;
                }
            }
        }
    }
}
