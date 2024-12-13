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

        #region Awake

        private void Awake()
        {
            InitializeSkinInfos.Instance.InitializeSkinObjects(SkinObjectsMatrix);
            CheckAndInitializeSkinInfo();
            ActivateEquippedItem();
        }

        #endregion


        public void ActivateGroupItems()
        {
            List<GameObject> tempList = SkinObjectsMatrix[ActiveSkinGroup];
            tempList.ForEach(skin => skin.SetActive(false));
            tempList[ActiveSkinIndex].SetActive(true);
            StoreManager.Instance.UpdateSkinPrice(SkinInfoMatrix[ActiveSkinGroup][ActiveSkinIndex].Price);
            StoreManager.Instance.UpdateButtonStatus(SkinInfoMatrix[ActiveSkinGroup], ActiveSkinIndex);
        }


        public void DeactivateGroupItem(int group, int index)
        {
            SkinObjectsMatrix[group][index].SetActive(false);
        }


        public void DeactivateGroupItems()
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


        private void CheckAndInitializeSkinInfo()
        {
            if (!BinaryData.IsSaveDataExits("CheckSkinSave"))
            {
                InitializeSkinInfos.Instance.SaveAndInitializeSkinInfo(SkinInfoMatrix);
            }
            else
            {
                LoadSkinInfoMatrix();
            }
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
    }
}