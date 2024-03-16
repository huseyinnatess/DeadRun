using System.Collections.Generic;
using MonoSingleton;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using Utilities.Store;
using Utilities.Store.Skin;

namespace Manager.Store
{
    public class HerosStoreManager : MonoSingleton<HerosStoreManager>
    {
        public List<GameObject> Heros;
        public List<StoreInformations> herosInfos = new List<StoreInformations>();
        [HideInInspector] public int CurrentIndex = 0;

        private Button _purchasseButton;
        private Button _equipButton;
        private Button _equippedButton;
        private Text _priceText;
        private TextMeshProUGUI _characterNameText;

        private void Awake()
        {
            GetReferences();
            SetReferences();
        }

        private void GetReferences()
        {
            _purchasseButton = GameObject.FindWithTag("PurchasseButton").GetComponent<Button>();
            _equipButton = GameObject.FindWithTag("EquipButton").GetComponent<Button>();
            _equippedButton = GameObject.FindWithTag("EquippedButton").GetComponent<Button>();
            _priceText = GameObject.FindWithTag("PriceText").GetComponent<Text>();
            _characterNameText = GameObject.FindWithTag("CharacterName").GetComponent<TextMeshProUGUI>();
        }


        private void SetReferences()
        {
            InitalizeList();
        }

        private void InitalizeList()
        {
            for (int i = 0; i < Heros.Count; i++)
            {
                Heros[i].SetActive(false);
                string[] parts = Heros[i].name.Split(' ');
                herosInfos[i] = new StoreInformations(0, parts[0], parts[1], false, false, _purchasseButton, _equipButton,
                    _equippedButton);
                UpdateButtonStatus();
                UpdateInformation();
            }
            _characterNameText.text = herosInfos[CurrentIndex].Name;
            Heros[CurrentIndex].SetActive(true);
        }

        public void UpdateInformation()
        {
            _characterNameText.text = herosInfos[CurrentIndex].Name;
            _priceText.text = herosInfos[CurrentIndex].Price;
        }

        public void UpdateButtonStatus()
        {
            bool boughtStatus = herosInfos[CurrentIndex].IsBought;
            herosInfos[CurrentIndex].PurchasseButton.gameObject.SetActive(!boughtStatus);
            herosInfos[CurrentIndex].EquippedButton.gameObject.SetActive(herosInfos[CurrentIndex].IsEquipped);
            herosInfos[CurrentIndex].EquipButton.gameObject.SetActive(boughtStatus);
        }

        public void EquipButtonStatus()
        {
            for (int i = 0; i < Heros.Count; i++)
            {
                herosInfos[i].IsEquipped = false;
            }

            herosInfos[CurrentIndex].IsEquipped = true;
            UpdateButtonStatus();
        }

        public void SetBought(bool check)
        {
            herosInfos[CurrentIndex].IsBought = check;
            UpdateButtonStatus();
        }
    }
}

