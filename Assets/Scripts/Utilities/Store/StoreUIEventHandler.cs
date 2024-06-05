using System;
using Manager;
using Manager.Store;
using UnityEngine;
using UnityEngine.UI;
using Utilities.SaveLoad;
using Utilities.UIElements;

namespace Utilities.Store
{
    public class StoreUIEventHandler : MonoBehaviour
    {
        /// <summary>
        /// Market içerisinde hero'lar arasında geçiş yapıp buton durumlarını ve
        /// aktif hero, skinleri günceller.
        /// </summary>
        public void NextButton()
        {
            HerosStoreManager herosStoreManager = HerosStoreManager.Instance;
            int currentIndex = herosStoreManager.CurrentIndex;
            int maxIndex = herosStoreManager.HerosObjects.Count - 1;

            herosStoreManager.HerosObjects[currentIndex].SetActive(false);
            currentIndex = (currentIndex + 1) % (maxIndex + 1);
            herosStoreManager.CurrentIndex = currentIndex;
            PlayerPrefsData.SetInt("CurrentIndex", currentIndex);
            SkinStoreManager.Instance.DeactivateGroupItems(currentIndex);
            StoreManager.Instance.ToggleSlotPanel(Convert.ToBoolean(currentIndex));
            herosStoreManager.HerosObjects[currentIndex].SetActive(true);
            herosStoreManager.UpdatePriceName();
            
            StoreManager.Instance.UpdateButtonStatus(HerosStoreManager.Instance.HerosInfos,
                HerosStoreManager.Instance.CurrentIndex);
        }
        
        /// <summary>
        /// Market içerisinde hero'lar arasında geçiş yapıp buton durumlarını ve
        /// aktif hero, skinleri günceller.
        /// </summary>
        public void BackButton()
        {
            HerosStoreManager herosStoreManager = HerosStoreManager.Instance;
            int currentIndex = herosStoreManager.CurrentIndex;
            int maxIndex = herosStoreManager.HerosObjects.Count - 1;

            herosStoreManager.HerosObjects[currentIndex].SetActive(false);
            currentIndex = (currentIndex - 1 + (maxIndex + 1)) % (maxIndex + 1);
            herosStoreManager.CurrentIndex = currentIndex;
            PlayerPrefsData.SetInt("CurrentIndex", currentIndex);
            StoreManager.Instance.ToggleSlotPanel(Convert.ToBoolean(currentIndex));
            SkinStoreManager.Instance.DeactivateGroupItems(currentIndex);
            herosStoreManager.HerosObjects[currentIndex].SetActive(true);
            herosStoreManager.UpdatePriceName();
            
            StoreManager.Instance.UpdateButtonStatus(HerosStoreManager.Instance.HerosInfos,
                HerosStoreManager.Instance.CurrentIndex);
        }
        
        /// <summary>
        /// Hero ve skinlerin satın alma butonu. Satın alınma durumlarını
        /// güncelleyip dosyaya kaydeder.
        /// </summary>
        /// <param name="priceText"> Fiyat text'i </param>
        public void PurchaseButton(Text priceText)
        {
            int price = Convert.ToInt32(priceText.text);
            bool stat = CoinManager.Instance.ProcessPurchase(price);
            if (SkinStoreManager.Instance.SkinObjectsMatrix[SkinStoreManager.ActiveSkinGroup][SkinStoreManager.ActiveSkinIndex]
                .activeInHierarchy)
            {
                StoreManager.Instance.SetIsBought(
                    SkinStoreManager.Instance.SkinInfoMatrix[SkinStoreManager.ActiveSkinGroup],
                    SkinStoreManager.ActiveSkinIndex, stat);
                BinaryData.Save(SkinStoreManager.Instance.SkinInfoMatrix[SkinStoreManager.ActiveSkinGroup], "SkinGroup" + SkinStoreManager.ActiveSkinGroup);
            }
            else
            {
                StoreManager.Instance.SetIsBought(HerosStoreManager.Instance.HerosInfos,
                    HerosStoreManager.Instance.CurrentIndex, stat);
                BinaryData.Save(HerosStoreManager.Instance.HerosInfos, "HerosInfos");
            }
        }
        
        /// <summary>
        /// Satın alınmış hero veya skinlerin kuşanma butonu.
        /// </summary>
        public void EquipButton()
        {
            if (SkinStoreManager.Instance.SkinObjectsMatrix[SkinStoreManager.ActiveSkinGroup][SkinStoreManager.ActiveSkinIndex]
                .activeInHierarchy)
            {
                StoreManager.Instance.EquipButtonStatus(
                    SkinStoreManager.Instance.SkinInfoMatrix[SkinStoreManager.ActiveSkinGroup],
                    SkinStoreManager.ActiveSkinIndex);
                BinaryData.Save(SkinStoreManager.Instance.SkinInfoMatrix[SkinStoreManager.ActiveSkinGroup], "SkinGroup" + SkinStoreManager.ActiveSkinGroup);
            }
            else
            {
                StoreManager.Instance.EquipButtonStatus(HerosStoreManager.Instance.HerosInfos,
                    HerosStoreManager.Instance.CurrentIndex);
                BinaryData.Save(HerosStoreManager.Instance.HerosInfos, "HerosInfos");
                HerosStoreManager.Instance.ActiveIndex = HerosStoreManager.Instance.CurrentIndex;
                PlayerPrefsData.SetInt("ActiveHeroIndex",  HerosStoreManager.Instance.ActiveIndex);
            }
        }
        
        /// <summary>
        /// Kuşanılmış hero veya skinler'i geri çıkarma butonu.
        /// </summary>
        public void EquippedButton()
        {
            if (SkinStoreManager.Instance.SkinObjectsMatrix[SkinStoreManager.ActiveSkinGroup][SkinStoreManager.ActiveSkinIndex]
                .activeInHierarchy)
            {
                StoreManager.Instance.EquippedButtonStatus(
                    SkinStoreManager.Instance.SkinInfoMatrix[SkinStoreManager.ActiveSkinGroup],
                    SkinStoreManager.ActiveSkinIndex);
                BinaryData.Save(SkinStoreManager.Instance.SkinInfoMatrix[SkinStoreManager.ActiveSkinGroup], "SkinGroup" + SkinStoreManager.ActiveSkinGroup);
                SkinStoreManager.Instance.DeactivateGroupItem(SkinStoreManager.ActiveSkinGroup, SkinStoreManager.ActiveSkinIndex);
            }
        }
        
        /// <summary>
        /// Anamenü'ye dönme butonu.
        /// </summary>
        public void MainMenuButton()
        {
            LoadingSlider.Instance.StartLoad(PlayerPrefsData.GetInt("EndLevel"));
        }
    }
}