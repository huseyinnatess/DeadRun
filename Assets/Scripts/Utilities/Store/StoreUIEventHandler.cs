using System;
using Manager;
using Manager.Store;
using UnityEngine;

namespace Utilities.Store
{
    public class StoreUIEventHandler : MonoBehaviour
    {
        public void NextButton()
        {
            if (++HerosStoreManager.Instance.CurrentIndex < HerosStoreManager.Instance.Heros.Count)
            {
                HerosStoreManager.Instance.Heros[HerosStoreManager.Instance.CurrentIndex - 1].SetActive(false);
                HerosStoreManager.Instance.Heros[HerosStoreManager.Instance.CurrentIndex].SetActive(true);
            }
            else
            {
                HerosStoreManager.Instance.Heros[HerosStoreManager.Instance.CurrentIndex - 1].SetActive(false);
                HerosStoreManager.Instance.CurrentIndex = 0;
                HerosStoreManager.Instance.Heros[HerosStoreManager.Instance.CurrentIndex].SetActive(true);
            }
            HerosStoreManager.Instance.UpdateInformation();
            HerosStoreManager.Instance.UpdateButtonStatus();
        }

        public void BackButton()
        {
            if (--HerosStoreManager.Instance.CurrentIndex >= 0)
            {
                HerosStoreManager.Instance.Heros[HerosStoreManager.Instance.CurrentIndex + 1].SetActive(false);
                HerosStoreManager.Instance.Heros[HerosStoreManager.Instance.CurrentIndex].SetActive(true);
            }
            else
            {
                HerosStoreManager.Instance.Heros[HerosStoreManager.Instance.CurrentIndex + 1].SetActive(false);
                HerosStoreManager.Instance.CurrentIndex = HerosStoreManager.Instance.Heros.Count - 1;
                HerosStoreManager.Instance.Heros[HerosStoreManager.Instance.CurrentIndex].SetActive(true);
            }
            HerosStoreManager.Instance.UpdateInformation();
            HerosStoreManager.Instance.UpdateButtonStatus();
        }

        public void PurchaseButton()
        {
            int index = HerosStoreManager.Instance.CurrentIndex;
            int price = Convert.ToInt32(HerosStoreManager.Instance.herosInfos[index].Price);
            bool stat = CoinManager.Instance.ProcessPurchase(price);
           HerosStoreManager.Instance.SetBought(stat); 
        }

        public void EquipButton()
        {
            HerosStoreManager.Instance.EquipButtonStatus();
        }
    }
}