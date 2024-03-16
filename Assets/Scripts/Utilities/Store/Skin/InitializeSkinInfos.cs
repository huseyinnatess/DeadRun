using System;
using System.Collections.Generic;
using MonoSingleton;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.Store.Skin
{
    public class InitializeSkinInfos : MonoSingleton<InitializeSkinInfos>
    {
        public List<GameObject> HatsSkin = new List<GameObject>();
        public List<GameObject> SwordsSkin = new List<GameObject>();
        public List<GameObject> ArmorsSkins = new List<GameObject>();
        public List<List<GameObject>> SkinsObjects = new List<List<GameObject>>();

        public List<StoreInformations> HatsInfo = new List<StoreInformations>(3);
        public List<StoreInformations> SwordsInfo = new List<StoreInformations>();
        public List<StoreInformations> ArmorsInfo = new List<StoreInformations>();
        
        private int _groupIndex = 0;
        private Button _purchasseButton;
        private Button _equipButton;
        private Button _equippedButton;

        private void Awake()
        {
            _purchasseButton = GameObject.FindWithTag("PurchasseButton").GetComponent<Button>();
            _equipButton = GameObject.FindWithTag("EquipButton").GetComponent<Button>();
            _equippedButton = GameObject.FindWithTag("EquippedButton").GetComponent<Button>();
        }

        private void InitializeSkinInfo(List<StoreInformations> skinInfolist, List<GameObject> skinObjects, List<List<StoreInformations>> mainList, List<List<GameObject>> mainObjects)
        {
            for (int i = 0; i < skinObjects.Count; i++)
            {
                string[] parts = skinObjects[i].name.Split(' ');
                skinInfolist.Add(new StoreInformations(_groupIndex, parts[0], parts[1], false, false, _purchasseButton, _equipButton,
                    _equippedButton));
            }
            mainList.Add(skinInfolist);
            mainObjects.Add(skinObjects);
            _groupIndex++;
        }

        public void SkinInfoList( List<List<StoreInformations>> mainList, List<List<GameObject>> mainObjects)
        {
            InitializeSkinInfo(HatsInfo, HatsSkin, mainList, mainObjects);
            InitializeSkinInfo(SwordsInfo, SwordsSkin, mainList, mainObjects);
            InitializeSkinInfo(ArmorsInfo, ArmorsSkins, mainList, mainObjects);
        }
    }
}