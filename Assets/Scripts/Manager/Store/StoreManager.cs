using System;
using System.Collections.Generic;
using MonoSingleton;
using UnityEngine;
using Utilities.Store;
using UnityEngine.UI;
using Utilities.SaveLoad;

namespace Manager.Store
{
    public class StoreManager : MonoSingleton<StoreManager>
    {
        private Button _purchasseButton;
        private Button _equipButton;
        private Button _equippedButton;
        private GameObject _skinPanel;
        private Text _priceText;
        private bool _isBought;

        #region Awake, Get Functions

        private void Awake()
        {
            GetReferences();
            SkinPanel(Convert.ToBoolean(PlayerPrefsData.GetInt("CurrentIndex")));
        }

        private void GetReferences()
        {
            _purchasseButton = GameObject.FindWithTag("PurchasseButton").GetComponent<Button>();
            _equipButton = GameObject.FindWithTag("EquipButton").GetComponent<Button>();
            _equippedButton = GameObject.FindWithTag("EquippedButton").GetComponent<Button>();
            _skinPanel = GameObject.FindWithTag("SkinSlots");
            _priceText = _purchasseButton.GetComponentInChildren<Text>();
        }

        #endregion


        public void UpdateButtonStatus(List<StoreInformations> infoList, int index)
        {
            _isBought = infoList[index].IsBought;
            _purchasseButton.gameObject.SetActive(!_isBought);
            _equipButton.gameObject.SetActive(_isBought);
            _equippedButton.gameObject.SetActive(infoList[index].IsEquipped);
        }


        public void EquipButtonStatus(List<StoreInformations> infoList, int index)
        {
            infoList.ForEach(info => info.IsEquipped = false);
            infoList[index].IsEquipped = true;
            UpdateButtonStatus(infoList, index);
        }


        public void EquippedButtonStatus(List<StoreInformations> infoList, int index)
        {
            infoList[index].IsEquipped = false;
            UpdateButtonStatus(infoList, index);
        }


        public void SetIsBought(List<StoreInformations> infoList, int index, bool check)
        {
            infoList[index].IsBought = check;
            UpdateButtonStatus(infoList, index);
        }


        public void UpdateSkinPrice(string price)
        {
            _priceText.text = price;
        }


        public void SkinPanel(bool state)
        {
            _skinPanel.SetActive(!state);
        }
    }
}