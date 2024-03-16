using System;
using System.Collections.Generic;
using MonoSingleton;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Store.Skin;

namespace Manager.Store
{
    public class SkinStoreManager : MonoSingleton<SkinStoreManager>
    {
        public List<List<GameObject>> SkinObjectsMatrix = new List<List<GameObject>>();
        public List<List<StoreInformations>> SkinInfoMatrix = new List<List<StoreInformations>>();

        [HideInInspector] public static int ActiveGroup;
        [HideInInspector] public static int ActiveIndex;
        
        private Text _priceText;
        private void Awake()
        {
            InitializeSkinInfos.Instance.SkinInfoList(SkinInfoMatrix, SkinObjectsMatrix);
            _priceText = GameObject.FindWithTag("PriceText").GetComponent<Text>();
        }
        
        public void ActivateGroupItems()
        {
            List<GameObject> tempList = SkinObjectsMatrix[ActiveGroup];
            for (int i = 0; i < tempList.Count; i++)
            {
                tempList[i].SetActive(false);
            }
            tempList[ActiveIndex].SetActive(true);
            UpdatePrice();
            StoreManager.Instance.UpdateButtonStatus(SkinInfoMatrix[ActiveGroup], ActiveIndex);
        }

        public void UpdatePrice()
        {
           _priceText.text = SkinInfoMatrix[ActiveGroup][ActiveIndex].Price;
        }
    }
}