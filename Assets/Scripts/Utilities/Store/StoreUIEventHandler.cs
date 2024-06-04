using System;
using Manager;
using Manager.Store;
using UnityEngine;
using UnityEngine.UI;
using Utilities.SaveLoad;

namespace Utilities.Store
{
    public class StoreUIEventHandler : MonoBehaviour
    {
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

        public void MainMenuButton()
        {
            LoadingSlider.Instance.StartLoad(PlayerPrefsData.GetInt("EndLevel"));
        }
    }
}