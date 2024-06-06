using System;
using Manager.Store;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.Store.Skin
{
    public class SkinButtonsEventHandler : MonoBehaviour
    {
        /// <summary>
        /// Buton isimlerindeki ilk sayı item'ın Group'u
        /// ikinci sayı ise index'i. Buna göre aktif edilecek
        /// item belirleniyor.
        /// </summary>
        /// <param name="button">Inspector'de item butonu</param>
        public void GetButtonName(Button button)
        {
            string[] parts = button.name.Split(' ');
            SkinStoreManager.ActiveSkinGroup = Convert.ToInt32(parts[0]);
            SkinStoreManager.ActiveSkinIndex = Convert.ToInt32(parts[1]);
            SkinStoreManager.Instance.ActivateGroupItems();
        }
    }
}