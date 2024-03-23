using System.Collections.Generic;
using MonoSingleton;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utilities.SaveLoad;
using Utilities.Store;


namespace Manager.Store
{
    public class HerosStoreManager : MonoSingleton<HerosStoreManager>
    {
        public List<GameObject> HerosObjects;
        public List<StoreInformations> HerosInfos = new List<StoreInformations>();
        [HideInInspector] public int CurrentIndex;
        [HideInInspector] public int ActiveIndex;

        private Text _priceText;
        private TextMeshProUGUI _characterNameText;

        #region Awake, Set, Get Functions

        private void Awake()
        {
            CurrentIndex = PlayerData.GetInt("CurrentIndex");
            ActiveIndex = PlayerData.GetInt("ActiveHeroIndex");
            GetReferences();
            SetReferences();
        }

        private void GetReferences()
        {
            _priceText = GameObject.FindWithTag("PriceText").GetComponent<Text>();
            _characterNameText = GameObject.FindWithTag("CharacterName").GetComponent<TextMeshProUGUI>();
        }

        private void SetReferences()
        {
            if (!BinaryData.IsSaveDataExits("HerosInfos"))
            {
                InitializeAndSaveHerosInfos();
            }
            else
            {
                LoadHerosInfos();
                SetHerosObjects();
                StoreManager.Instance.UpdateButtonStatus(HerosInfos, CurrentIndex);
                UpdatePriceName();
            }
        }
        #endregion

        #region Load

        private void LoadHerosInfos()
        {
            HerosInfos = BinaryData.Load("HerosInfos");
        }
        
        private void SetHerosObjects()
        {
            for (int i = 0; i < HerosObjects.Count; i++)
            {
                HerosObjects[i].SetActive(false);
            }
            HerosObjects[CurrentIndex].SetActive(true);
        }
        #endregion

        #region InitiliazeAndSave
        private void InitalizeList()
        {
            for (int i = 0; i < HerosObjects.Count; i++)
            {
                HerosObjects[i].SetActive(false);
                string[] parts = HerosObjects[i].name.Split(' ');
                HerosInfos[i] = new StoreInformations(0, parts[0], parts[1], false, false);
                StoreManager.Instance.UpdateButtonStatus(HerosInfos, CurrentIndex);
                UpdatePriceName();
            }

            _characterNameText.text = HerosInfos[CurrentIndex].Name;
            HerosObjects[0].SetActive(true);
            HerosInfos[0].IsBought = true;
            HerosInfos[0].IsEquipped = true;
            StoreManager.Instance.UpdateButtonStatus(HerosInfos, CurrentIndex);
        }
        private void InitializeAndSaveHerosInfos()
        {
            InitalizeList();
            BinaryData.Save(HerosInfos, "HerosInfos");
        }

        #endregion
        
        public void UpdatePriceName()
        {
            _characterNameText.text = HerosInfos[CurrentIndex].Name;
            _priceText.text = HerosInfos[CurrentIndex].Price;
        }
    }
}