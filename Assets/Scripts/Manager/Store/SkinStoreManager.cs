using System;
using System.Collections.Generic;
using MonoSingleton;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Store;
using Utilities.Store.Skin;

namespace Manager.Store
{
    public class SkinStoreManager : MonoSingleton<SkinStoreManager>
    {
        public List<List<GameObject>> SkinObjectsMatrix = new List<List<GameObject>>();
        public List<List<StoreInformations>> SkinInfoMatrix = new List<List<StoreInformations>>();

        public static int ActiveGroup;
        public static int ActiveIndex;
        
        private Text _priceText;
        private GameObject _skinSlots;
        private void Awake()
        {
            InitializeSkinInfos.Instance.SkinInfoList(SkinInfoMatrix, SkinObjectsMatrix);
            _skinSlots = GameObject.FindWithTag("SkinSlots");
        }
        
        public void ActivateGroupItems()
        {
            List<GameObject> tempList = SkinObjectsMatrix[ActiveGroup];
            for (int i = 0; i < tempList.Count; i++)
            {
                tempList[i].SetActive(false);
            }
            tempList[ActiveIndex].SetActive(true);
           StoreManager.Instance.UpdateSkinPrice(SkinInfoMatrix[ActiveGroup][ActiveIndex].Price);
            StoreManager.Instance.UpdateButtonStatus(SkinInfoMatrix[ActiveGroup], ActiveIndex);
        }

        public void DeactivateGroupItem(int group, int index)
        {
            SkinObjectsMatrix[group][index].SetActive(false);
        }
        
        public void DeactivateGroupItems(int activeHeroIndex)
        {
            if (activeHeroIndex != 0)
            {
                _skinSlots.SetActive(false);
                return;
            }
            _skinSlots.SetActive(true);
            for (int i = 0; i < SkinObjectsMatrix.Count; i++)
            {
                for (int j = 0; j < SkinObjectsMatrix[i].Count; j++)
                {
                    if (SkinObjectsMatrix[i][j].activeSelf && !SkinInfoMatrix[i][j].IsEquipped)
                    {
                        DeactivateGroupItem(i, j);
                        break;
                    }
                }
            }
        }
    }
}