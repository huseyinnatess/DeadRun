using System.Collections.Generic;
using MonoSingleton;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities.SaveLoad;
using Utilities.Store;

namespace Manager.Store
{
    public class HerosStoreManager : MonoSingleton<HerosStoreManager>
    {
        public List<GameObject> HerosObjects;
        public List<StoreInformations> herosInfos = new List<StoreInformations>();
        [HideInInspector] public int CurrentIndex = 0;


        private Text _priceText;
        private TextMeshProUGUI _characterNameText;

        #region Awake, Set, Get Functions

        private void Awake()
        {
            GetReferences();
            SetReferences();
            BinaryData.Save(herosInfos, herosInfos.ToString());
        }

        private void GetReferences()
        {
            _priceText = GameObject.FindWithTag("PriceText").GetComponent<Text>();
            _characterNameText = GameObject.FindWithTag("CharacterName").GetComponent<TextMeshProUGUI>();
        }


        private void SetReferences()
        {
            InitalizeList();
        }

        #endregion


        private void InitalizeList()
        {
            for (int i = 0; i < HerosObjects.Count; i++)
            {
                HerosObjects[i].SetActive(false);
                string[] parts = HerosObjects[i].name.Split(' ');
                herosInfos[i] = new StoreInformations(0, parts[0], parts[1], false, false);
                StoreManager.Instance.UpdateButtonStatus(herosInfos, CurrentIndex);
                UpdatePriceName();
            }

            _characterNameText.text = herosInfos[CurrentIndex].Name;
            HerosObjects[0].SetActive(true);
            herosInfos[0].IsBought = true;
            herosInfos[0].IsEquipped = true;
            StoreManager.Instance.UpdateButtonStatus(herosInfos, CurrentIndex);
        }

        public void UpdatePriceName()
        {
            _characterNameText.text = herosInfos[CurrentIndex].Name;
            _priceText.text = herosInfos[CurrentIndex].Price;
        }
    }
}