using System;
using Manager;
using Manager.Store;
using UnityEngine;
using UnityEngine.UI;

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
            
            SkinStoreManager.Instance.DeactivateGroupItems(currentIndex);
            herosStoreManager.HerosObjects[currentIndex].SetActive(true);
            herosStoreManager.UpdatePriceName();
            
            StoreManager.Instance.UpdateButtonStatus(HerosStoreManager.Instance.herosInfos,
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
            
            SkinStoreManager.Instance.DeactivateGroupItems(currentIndex);
            herosStoreManager.HerosObjects[currentIndex].SetActive(true);
            herosStoreManager.UpdatePriceName();
            
            StoreManager.Instance.UpdateButtonStatus(HerosStoreManager.Instance.herosInfos,
                HerosStoreManager.Instance.CurrentIndex);
        }

        public void PurchaseButton(Text priceText)
        {
            int price = Convert.ToInt32(priceText.text);
            bool stat = CoinManager.Instance.ProcessPurchase(price);
            if (SkinStoreManager.Instance.SkinObjectsMatrix[SkinStoreManager.ActiveGroup][SkinStoreManager.ActiveIndex]
                .activeInHierarchy)
            {
                StoreManager.Instance.SetIsBought(
                    SkinStoreManager.Instance.SkinInfoMatrix[SkinStoreManager.ActiveGroup],
                    SkinStoreManager.ActiveIndex, stat);
            }
            else
            {
                StoreManager.Instance.SetIsBought(HerosStoreManager.Instance.herosInfos,
                    HerosStoreManager.Instance.CurrentIndex, stat);
            }
        }

        public void EquipButton()
        {
            if (SkinStoreManager.Instance.SkinObjectsMatrix[SkinStoreManager.ActiveGroup][SkinStoreManager.ActiveIndex]
                .activeInHierarchy)
            {
                StoreManager.Instance.EquipButtonStatus(
                    SkinStoreManager.Instance.SkinInfoMatrix[SkinStoreManager.ActiveGroup],
                    SkinStoreManager.ActiveIndex);
            }
            else
            {
                StoreManager.Instance.EquipButtonStatus(HerosStoreManager.Instance.herosInfos,
                    HerosStoreManager.Instance.CurrentIndex);
            }
        }

        public void EquippedButton()
        {
            if (SkinStoreManager.Instance.SkinObjectsMatrix[SkinStoreManager.ActiveGroup][SkinStoreManager.ActiveIndex]
                .activeInHierarchy)
            {
                StoreManager.Instance.EquippedButtonStatus(
                    SkinStoreManager.Instance.SkinInfoMatrix[SkinStoreManager.ActiveGroup],
                    SkinStoreManager.ActiveIndex);
                SkinStoreManager.Instance.DeactivateGroupItem(SkinStoreManager.ActiveGroup, SkinStoreManager.ActiveIndex);
            }
        }
    }
}