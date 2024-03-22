using UnityEngine.UI;

namespace Utilities.Store
{
    [System.Serializable]
    public class StoreInformations
    {
            public string Name { get; set; }
            public int GroupIndex { get; set; }
            
            public string Price;
            public bool IsBought { get; set; }
            public bool IsEquipped { get; set; }

            public StoreInformations(int groupIndex, string name, string price, bool bought, bool equipped)
            {
                Name = name;
                Price = price;
                IsBought = bought;
                IsEquipped = equipped;
                GroupIndex = groupIndex;
            }
    }
}