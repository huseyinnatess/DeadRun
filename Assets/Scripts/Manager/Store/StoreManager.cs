using System.Collections.Generic;
using MonoSingleton;
using TMPro;
using UnityEngine.UI;
using Utilities.Store.Skin;

namespace Manager.Store
{
    public class StoreManager : MonoSingleton<StoreManager>
    {
        public void UpdateButtonStatus(List<StoreInformations> infoList, int index)
        {
            bool boughtStatus = infoList[index].IsBought;
            infoList[index].PurchasseButton.gameObject.SetActive(!boughtStatus);
            infoList[index].EquippedButton.gameObject.SetActive(infoList[index].IsEquipped);
            infoList[index].EquipButton.gameObject.SetActive(boughtStatus);
        }
        
        public void EquipButtonStatus(List<StoreInformations> infoList, int index)
        {
            for (int i = 0; i < infoList.Count; i++)
            {
                infoList[i].IsEquipped = false;
            }
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
    }
}