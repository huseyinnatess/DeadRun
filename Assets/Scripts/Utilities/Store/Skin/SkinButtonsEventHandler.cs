using System;
using Manager.Store;
using UnityEngine;
using UnityEngine.UI;
using Utilities.SaveLoad;

namespace Utilities.Store.Skin
{
    public class SkinButtonsEventHandler : MonoBehaviour
    {
        public void GetButtonName(Button button)
        {
            string[] parts = button.name.Split(' ');
            SkinStoreManager.ActiveSkinGroup = Convert.ToInt32(parts[0]);
            SkinStoreManager.ActiveSkinIndex = Convert.ToInt32(parts[1]);
            SkinStoreManager.Instance.ActivateGroupItems();
        }
    }
}