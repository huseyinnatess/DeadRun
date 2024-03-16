using System;
using System.Collections.Generic;
using MonoSingleton;
using UnityEngine;
using Utilities.Store;
using Utilities.Store.Skin;

namespace Manager.Store
{
    public class SkinStoreManager : MonoSingleton<SkinStoreManager>
    {
        public List<List<GameObject>> SkinObjectsMatrix = new List<List<GameObject>>();
        public List<List<StoreInformations>> SkinInfoMatrix = new List<List<StoreInformations>>();

        [HideInInspector] public static int ActiveGroup;
        [HideInInspector] public static int ActiveIndex;
        private void Awake()
        {
            InitializeSkinInfos.Instance.SkinInfoList(SkinInfoMatrix, SkinObjectsMatrix);
        }


        public void ActivateGroupItems()
        {
            List<GameObject> tempList = SkinObjectsMatrix[ActiveGroup];
            for (int i = 0; i < tempList.Count; i++)
            {
                tempList[i].SetActive(false);
            }
            tempList[ActiveIndex].SetActive(true);
        }
    }
}