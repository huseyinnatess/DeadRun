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
        public List<List<GameObject>> SkinObjectsMatrix = new(); // Skinler'in obje nesnelerinin listesi.
        public List<List<StoreInformations>> SkinInfoMatrix = new(); // Skinler'in bilgilerinin listesi.

        public static int ActiveSkinGroup; // Aktif skin grubu.
        public static int ActiveSkinIndex; // Aktif skin indexi.


        #region Awake
        private void Awake()
        {
            InitializeSkinInfos.Instance.InitializeSkinObjects(SkinObjectsMatrix);
            CheckAndInitializeSkinInfo();
            ActivateEquippedItem();
        }
        #endregion
        
        /// <summary>
        /// Aktif olan skin haricindeki skinleri kapatıp, açık olan skin'in buton bilgilerini günceller.
        /// </summary>
        public void ActivateGroupItems()
        {
            List<GameObject> tempList = SkinObjectsMatrix[ActiveSkinGroup];
            tempList.ForEach(skin => skin.SetActive(false));
            tempList[ActiveSkinIndex].SetActive(true);
            StoreManager.Instance.UpdateSkinPrice(SkinInfoMatrix[ActiveSkinGroup][ActiveSkinIndex].Price);
            StoreManager.Instance.UpdateButtonStatus(SkinInfoMatrix[ActiveSkinGroup], ActiveSkinIndex);
        }
        
        /// <summary>
        /// Belirli bir skinin aktifliğini kapatır.
        /// </summary>
        /// <param name="group">Kapatılacak skin'in grubu.</param>
        /// <param name="index">Kapatılacak skin'in indexi.</param>
        public void DeactivateGroupItem(int group, int index)
        {
            SkinObjectsMatrix[group][index].SetActive(false);
        }
        
        /// <summary>
        /// Oyunda aktif olup kuşanılmamış tüm skinleri deaktif eder.
        /// </summary>
        public void DeactivateGroupItems()
        {
            print(ActiveSkinGroup);
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
        
        // Oyun ilk başladığında save dosyası oluşturup sonraki açılışlarda 
        // load işlemi yapar.
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
        
        // Tüm skin bilgilerini dosyadan geri yükleme işlemi yapar.
        private void LoadSkinInfoMatrix()
        {
            for (int i = 0; i < InitializeSkinInfos.ListCount; i++)
            {
                List<StoreInformations> list = BinaryData.Load("SkinGroup" + i);
                SkinInfoMatrix.Add(list);
            }
        }
        
        // Kuşanılmış olan skinler'in aktifliğini açar.
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