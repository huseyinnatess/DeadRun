using System.Collections.Generic;
using MonoSingleton;
using UnityEngine;
using Utilities.SaveLoad;
using Utilities.Store;
using Utilities.Store.Skin;

namespace Manager.Store
{
    public class SkinStoreManager : MonoSingleton<SkinStoreManager>
    {
        public List<List<GameObject>> SkinObjectsMatrix = new();
        public List<List<StoreInformations>> SkinInfoMatrix = new();

        public static int ActiveSkinGroup;
        public static int ActiveSkinIndex;


        #region Awake, Save, Load, Initilaze Functions

        private void Awake()
        {
            InitializeSkinInfos.Instance.InitializeSkinObjects(SkinObjectsMatrix);
            CheckAndInitializeSkinInfo();
            ActivateEquippedItem();
        }

        private void CheckAndInitializeSkinInfo()
        {
            if (!BinaryData.IsSaveDataExits("CheckSkinSave"))
            {
                SaveAndInitializeSkinInfo();
            }
            else
            {
                LoadSkinInfoMatrix();
            }
        }

        private void SaveAndInitializeSkinInfo()
        {
            InitializeSkinInfos.Instance.SaveAndInitializeSkinInfo(SkinInfoMatrix);
        }

        private void LoadSkinInfoMatrix()
        {
            for (int i = 0; i < InitializeSkinInfos.ListCount; i++)
            {
                List<StoreInformations> list = BinaryData.Load("SkinGroup" + i);
                SkinInfoMatrix.Add(list);
            }
        }

        private void ActivateEquippedItem()
        {
            for (int i = 0; i < SkinInfoMatrix.Count; i++)
            {
                for (int j = 0; j < SkinInfoMatrix[i].Count; j++)
                {
                    if (SkinInfoMatrix[i][j].IsEquipped)
                    {
                        SkinObjectsMatrix[i][j].SetActive(true);
                    }
                }
            }
        }

        #endregion

        public void ActivateGroupItems()
        {
            List<GameObject> tempList = SkinObjectsMatrix[ActiveSkinGroup];
            for (int i = 0; i < tempList.Count; i++)
            {
                tempList[i].SetActive(false);
            }
            tempList[ActiveSkinIndex].SetActive(true);
            StoreManager.Instance.UpdateSkinPrice(SkinInfoMatrix[ActiveSkinGroup][ActiveSkinIndex].Price);
            StoreManager.Instance.UpdateButtonStatus(SkinInfoMatrix[ActiveSkinGroup], ActiveSkinIndex);
        }

        public void DeactivateGroupItem(int group, int index)
        {
            SkinObjectsMatrix[group][index].SetActive(false);
        }

        public void DeactivateGroupItems(int activeHeroIndex)
        {
            for (int i = 0; i < SkinObjectsMatrix.Count; i++)
            {
                for (int j = 0; j < SkinObjectsMatrix[i].Count; j++)
                {
                    if (SkinObjectsMatrix[i][j].activeSelf && !SkinInfoMatrix[i][j].IsEquipped)
                    {
                        DeactivateGroupItem(i, j);
                        break;
                    }
                }
            }
        }
    }
}