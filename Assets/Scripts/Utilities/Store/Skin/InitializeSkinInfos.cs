using System;
using System.Collections.Generic;
using MonoSingleton;
using UnityEngine;
using UnityEngine.UI;
using Utilities.SaveLoad;

namespace Utilities.Store.Skin
{
    public class InitializeSkinInfos : MonoSingleton<InitializeSkinInfos>
    {
        public List<GameObject> HatsSkin = new List<GameObject>();
        public List<GameObject> SwordsSkin = new List<GameObject>();
        public List<GameObject> ArmorsSkins = new List<GameObject>();

        public List<StoreInformations> HatsInfo = new List<StoreInformations>(3);
        public List<StoreInformations> SwordsInfo = new List<StoreInformations>();
        public List<StoreInformations> ArmorsInfo = new List<StoreInformations>();
        
        public static int ListCount = 3;
        
        private int _groupIndex = 0;
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

        public void InitializeSkinObjects(List<List<GameObject>> mainSkinObjectList)
        {
            InitializeSkinInfoObjects(HatsSkin, mainSkinObjectList);
            InitializeSkinInfoObjects(SwordsSkin, mainSkinObjectList);
            InitializeSkinInfoObjects(ArmorsSkins, mainSkinObjectList);
        }
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
        
        private void InitializeSkinInfoObjects(List<GameObject> skinObjects, List<List<GameObject>> mainSkinObjectList)
        {
            mainSkinObjectList.Add(skinObjects);
        }
    }
}