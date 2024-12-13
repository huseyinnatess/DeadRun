using System.Collections.Generic;
using System.Linq;
using MonoSingleton;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities.SaveLoad;
using Utilities.Store;


namespace Manager.Store
{
    public class HerosStoreManager : MonoSingleton<HerosStoreManager>
    {
        public List<GameObject> HerosObjects;
        public List<StoreInformations> HerosInfos = new();
        [HideInInspector] public int CurrentIndex;
        [HideInInspector] public int ActiveIndex; // Aktif olarak kullanılan hero index'i.

        private Text _priceText;
        private TextMeshProUGUI _characterNameText;

        #region Awake, Set, Get Functions

        private void Awake()
        {
            CurrentIndex = PlayerPrefsData.GetInt("CurrentIndex");
            ActiveIndex = PlayerPrefsData.GetInt("ActiveHeroIndex");
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

        #region Load Hero Informations

        private void LoadHerosInfos()
        {
            HerosInfos = BinaryData.Load("HerosInfos");
        }

        private void SetHerosObjects()
        {
            HerosObjects.ForEach(hero => hero.SetActive(false));
            HerosObjects.ElementAt(CurrentIndex).SetActive(true);
        }

        #endregion

        #region InitiliazeAndSave

        private void InitializeAndSaveHerosInfos()
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