using UnityEngine.UI;

namespace Utilities.Store.Skin
{
    [System.Serializable]
    public class StoreInformations
    {
            public string Name { get; set; }
            public int GroupIndex { get; set; }
            
            public string Price;
            public bool IsBought { get; set; }
            public bool IsEquipped { get; set; }
            public Button PurchasseButton { get; set; }
            public Button EquipButton { get; set; }
            public Button EquippedButton { get; set; }

            public StoreInformations(int groupIndex, string name, string price, bool bought, bool equipped, Button purchasseButton, Button equipButton,
                Button equippedButton)
            {
                Name = name;
                Price = price;
                IsBought = bought;
                IsEquipped = equipped;
                PurchasseButton = purchasseButton;
                EquipButton = equipButton;
                EquippedButton = equippedButton;
                GroupIndex = groupIndex;
            }
    }
}