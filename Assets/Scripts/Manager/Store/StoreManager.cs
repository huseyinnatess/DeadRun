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
        private Button _purchasseButton; // Satın alma butonu.
        private Button _equipButton; // Kuşanma butonu.
        private Button _equippedButton; // Kuşanma bilgisi ve aynı zamanda geri çıkarma butonu.
        
        private GameObject _skinPanel; // Skinler'in bulunduğu panel.

        private Text _priceText; // Skin ve hero'ların fiyatı.

        private bool _isBought; // Hero veya item'ların satın alınmış mı? değişkeni.

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
        
        /// <summary>
        /// İtem ve hero'ların butonlarının satın alınma, kuşanılma gibi bilgilerini
        /// günceller.
        /// </summary>
        /// <param name="infoList">Güncelleme yapılacak item veya hero'nun info listesi.</param>
        /// <param name="index">Güncelleme yapılacak item veya hero'nun index'i.</param>
        public void UpdateButtonStatus(List<StoreInformations> infoList, int index)
        {
             _isBought = infoList[index].IsBought;
            _purchasseButton.gameObject.SetActive(!_isBought);
            _equipButton.gameObject.SetActive(_isBought);
            _equippedButton.gameObject.SetActive(infoList[index].IsEquipped);
        }
        
        /// <summary>
        /// Daha önce kuşanılmış olan item veya hero'nun kuşanılma bilgisini yeni
        /// item veya hero kuşanıldığı zaman false yapıp butonunu günceller.
        /// </summary>
        /// <param name="infoList">Güncelleme yapılacak item veya hero'nun info listesi.</param>
        /// <param name="index">Güncelleme yapılacak item veya hero'nun index'i.</param>
        public void EquipButtonStatus(List<StoreInformations> infoList, int index)
        {
            infoList.ForEach(info => info.IsEquipped = false);
            infoList[index].IsEquipped = true;
            UpdateButtonStatus(infoList, index);
        }
        
        /// <summary>
        /// Kuşanılmış olan bir item'ın Equipped butonuna (Hero'larda geçerli değil)
        /// tekrar basılması durumunda kuşanılma bilgisini false yapıp butonunu günceller.
        /// </summary>
        /// <param name="infoList">Güncelleme yapılacak item veya hero'nun info listesi.</param>
        /// <param name="index">Güncelleme yapılacak item veya hero'nun index'i.</param>
        public void EquippedButtonStatus(List<StoreInformations> infoList, int index)
        {
            infoList[index].IsEquipped = false;
            UpdateButtonStatus(infoList, index);
        }
        
        /// <summary>
        /// İtem veya hero'ların satın alınma bilgilerini ve butonlarını günceller.
        /// </summary>
        /// <param name="infoList">Güncelleme yapılacak item veya hero'nun info listesi.</param>
        /// <param name="index">Güncelleme yapılacak item veya hero'nun index'i.</param>
        /// <param name="check">Satın alınma bilgisi.</param>
        public void SetIsBought(List<StoreInformations> infoList, int index, bool check)
        {
            infoList[index].IsBought = check;
            UpdateButtonStatus(infoList, index);
        }
        
        /// <summary>
        /// Butonlardaki item fiyatlarını günceller.
        /// </summary>
        /// <param name="price">İtem fiyatı.</param>
        public void UpdateSkinPrice(string price)
        {
            _priceText.text = price;
        }
        
        /// <summary>
        /// İtem'ların bulunduğu panelin aktifliğini ayarlar.
        /// </summary>
        /// <param name="state">Aktiflik durumu.</param>
        public void SkinPanel(bool state)
        {
            _skinPanel.SetActive(!state);
        }
    }
}