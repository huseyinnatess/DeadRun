using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Store.Skin
{
    public class InitializeSkinInfos : MonoBehaviour
    {
        public List<GameObject> HatsSkin = new List<GameObject>();
        public List<GameObject> SwordsSkin = new List<GameObject>();
        public List<GameObject> ArmorsSkins = new List<GameObject>();
        public List<List<GameObject>> SkinsObjects = new List<List<GameObject>>();

        public List<SkinInformations> HatsInfo = new List<SkinInformations>(3);
        public List<SkinInformations> SwordsInfo = new List<SkinInformations>();
        public List<SkinInformations> ArmorsInfo = new List<SkinInformations>();

        public static InitializeSkinInfos Instance;
        private int _groupIndex = 0;

        private void Awake()
        {
            if (!Instance)
                Instance = this;
        }

        private void InitializeSkinInfo(List<SkinInformations> list, List<GameObject> skinObjects, List<List<SkinInformations>> mainList, List<List<GameObject>> mainObjects)
        {
            for (int i = 0; i < skinObjects.Count; i++)
            {
                list.Add(new SkinInformations());
                list[i].GroupIndex = _groupIndex;
                list[i].IsBought = false;
                list[i].IsEquipped = false;
                string[] parts = skinObjects[i].name.Split(' ');
                list[i].Price = parts[1];
            }

            mainList.Add(list);
            mainObjects.Add(skinObjects);
            _groupIndex++;
        }

        public void SkinInfoList( List<List<SkinInformations>> mainList, List<List<GameObject>> mainObjects)
        {
            InitializeSkinInfo(HatsInfo, HatsSkin, mainList, mainObjects);
            InitializeSkinInfo(SwordsInfo, SwordsSkin, mainList, mainObjects);
            InitializeSkinInfo(ArmorsInfo, ArmorsSkins, mainList, mainObjects);
        }
    }
}