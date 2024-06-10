using System.Collections.Generic;
using MonoSingleton;
using UnityEngine;
using Utilities.SaveLoad;

namespace Utilities.Store.Skin
{
    public class InitializeSkinInfos : MonoSingleton<InitializeSkinInfos>
    {
        public List<GameObject> HatsSkin = new List<GameObject>(); // Şapkalar'ın obje listesi.
        public List<GameObject> SwordsSkin = new List<GameObject>(); // Kılıçlar'ın obje listesi.
        public List<GameObject> ArmorsSkins = new List<GameObject>(); // Zırhlar'ın obje listesi.

        public List<StoreInformations> HatsInfo = new List<StoreInformations>(3); // Şapka bilgilerini tutan liste.
        public List<StoreInformations> SwordsInfo = new List<StoreInformations>(); // Kılıç bilgilerini tutan liste.
        public List<StoreInformations> ArmorsInfo = new List<StoreInformations>(); // Zırh bilgilerini tutan liste.
        
        public static int ListCount = 3; // Listedeki eleman sayısı.
        
        private int _groupIndex = 0; // Listelerin group indexi.
        
        /// <summary>
        /// Parametre olarak gelen matris'in içerisine skin bilgilerini
        /// initliaze edip dosyaya save işlemi yapıyor.
        /// </summary>
        /// <param name="mainList">Initialize edilecek çift boyutlu liste.</param>
        public void SaveAndInitializeSkinInfo( List<List<StoreInformations>> mainList)
        {
            InitializeSkinInfo(HatsInfo, HatsSkin, mainList);
            BinaryData.Save(HatsInfo, "SkinGroup0");
            InitializeSkinInfo(SwordsInfo, SwordsSkin, mainList);
            BinaryData.Save(SwordsInfo, "SkinGroup1");
            InitializeSkinInfo(ArmorsInfo, ArmorsSkins, mainList);
            BinaryData.Save(ArmorsInfo, "SkinGroup2");
            BinaryData.Save(HatsInfo, "CheckSkinSave");
        }
            
        /// <summary>
        /// Parametre olarak gelen matris'in içerisine skinler'in GameObject'lerini ekler.
        /// </summary>
        /// <param name="mainSkinObjectList">Ekleme yapılacak olan çift boyutlu liste.</param>
        public void InitializeSkinObjects(List<List<GameObject>> mainSkinObjectList)
        {
            mainSkinObjectList.Add(HatsSkin);
            mainSkinObjectList.Add(SwordsSkin);
            mainSkinObjectList.Add(ArmorsSkins);
        }
        
        // Skin bilgilerini mainList içerisine Initialize eder.
        private void InitializeSkinInfo(List<StoreInformations> skinInfolist, List<GameObject> skinObjects, List<List<StoreInformations>> mainList)
        {
            for (int i = 0; i < skinObjects.Count; i++)
            {
                string[] parts = skinObjects[i].name.Split(' ');
                skinInfolist.Add(new StoreInformations(_groupIndex, null, parts[1], false, false));
            }
            mainList.Add(skinInfolist);
            _groupIndex++;
        }
    }
}