using System;
using Manager.Store;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.Store.Skin
{
    public class SkinButtonsEventHandler : MonoBehaviour
    {
        public void GetButtonName(Button button)
        {
            string[] parts = button.name.Split(' ');
            SkinStoreManager.ActiveGroup = Convert.ToInt32(parts[0]);
            SkinStoreManager.ActiveIndex = Convert.ToInt32(parts[1]);
            SkinStoreManager.Instance.ActivateGroupItems();
        }
    }
}