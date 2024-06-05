namespace Utilities.Store
{
    [System.Serializable]
    public class StoreInformations
    {
            public string Name { get; set; } // İtem ismi.
            public int GroupIndex { get; set; } // İtem grup index'i.
            
            public string Price; // İtem fiyatı
            public bool IsBought { get; set; } // Satın alınıp alınmadığı durumu.
            public bool IsEquipped { get; set; } // Kuşanılıp kuşanılmama durumu.

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