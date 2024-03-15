using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Store.Skin;

namespace Manager.Store
{
    public class SkinStoreManager : MonoBehaviour
    {
        public static SkinStoreManager Instance;
        public List<List<GameObject>> SkinObjectsMatrix = new List<List<GameObject>>();
        public List<List<SkinInformations>> SkinInfoMatrix = new List<List<SkinInformations>>();

        private void Awake()
        {
            if (!Instance)
                Instance = this;
            InitializeSkinInfos.Instance.SkinInfoList(SkinInfoMatrix, SkinObjectsMatrix);
        }
        
    }
}