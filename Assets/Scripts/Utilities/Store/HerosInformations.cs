using UnityEngine.UI;

namespace Utilities
{
    public class HerosInformations
    {
        [System.Serializable]
        public class HerosInfo
        {
            public string Name { get; set; }
            public string Price;
            public bool IsBought { get; set; }
            public bool IsEquipped { get; set; }
            public Button PurchasseButton { get; set; }
            public Button EquipButton { get; set; }
            public Button EquippedButton { get; set; }

            public HerosInfo(string name, string price, bool bought, bool equipped, Button purchasseButton, Button equipButton,
                Button equippedButton)
            {
                Name = name;
                Price = price;
                IsBought = bought;
                IsEquipped = equipped;
                PurchasseButton = purchasseButton;
                EquipButton = equipButton;
                EquippedButton = equippedButton;
            }
        }
    }
}