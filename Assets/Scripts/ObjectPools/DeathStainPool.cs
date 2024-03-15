using System.Collections.Generic;
using UnityEngine;

namespace ObjectPools
{
    public class DeathStainPool : MonoBehaviour
    {
        public GameObject[] deathStains;
        public static DeathStainPool Instance;

        private void Awake()
        {
            Singleton();
        }

        private void Singleton()
        {
            if (Instance == null)
                Instance = this;
        }

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
