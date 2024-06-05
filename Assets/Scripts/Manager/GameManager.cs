using System.Collections.Generic;
using Controller;
using MonoSingleton;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities.SaveLoad;
using Utilities.Store;
using Utilities.Store.Skin;

namespace Manager
{
    public class GameManager : MonoSingleton<GameManager>
    {
        private GameObject _handel; // Oyundaki sağa sola giden el nesnesi
        public static bool HandelIsActive; // Elin aktif olup olmadığını kontrol eder
        
       [SerializeField] private TextMeshProUGUI _sliderLevelText; // Game panel'deki level

        public List<GameObject> Heros; // Karakterlerin tutulduğu liste
        public List<GameObject> HatSkins; // Şapkaların listesi
        public List<GameObject> SwordSkins; // Kılıçların listesi
        public List<GameObject> ArmorSkins; // Zırhların listesi
        public List<List<GameObject>> HerosSkins; // Tüm skinlerin tutulduğu çift boyutlu liste

        #region Awake, Get, Set Functions
        private void Awake()
        {
            Time.timeScale = 1f;
            HandelIsActive = false;
            GetReferences();
            SetReferences();
            ActivateHero();
            InitializeSkins();
            ActivateHeroSkins();
        }

        private void GetReferences()
        {
            _handel = GameObject.FindWithTag("Handel");
        }

        private void SetReferences()
        {
            _sliderLevelText.text = "LEVEL " + (SceneManager.GetActiveScene().buildIndex - 1);
            ActivateHandle(false);
        }
        #endregion

        #region Update

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0) || !HandelIsActive) return;
            ActivateHandle(false);
            CharacterControl.Instance.enabled = true;
            HandelIsActive = false;
        }
        #endregion
        
        // Update
        /// <summary>
        /// Parametre olarak gelen state göre handle aktifliğini ayarlar
        /// </summary>
        /// <param name="state"> Handle aktifliği için</param>
        public void ActivateHandle(bool state) => _handel.SetActive(state);
        
        // Skin listelerini Initilaze eder
        private void InitializeSkins()
        {
            HerosSkins = new List<List<GameObject>> { HatSkins, SwordSkins, ArmorSkins };
        }
        
        // Seçilen hero'yu aktif edip diğer heroları kapatır
        private void ActivateHero()
        {
            int activeHeroIndex = PlayerPrefsData.GetInt("ActiveHeroIndex");

            for (int i = 0; i < Heros.Count; i++)
            {
                Heros[i].SetActive(i == activeHeroIndex);
            }
        }
        
        // Kuşanılmış olan skinleri aktif edip diğerlerini kapatır
        private void ActivateHeroSkins()
        {
            for (int i = 0; i < InitializeSkinInfos.ListCount; i++)
            {
                List<StoreInformations> informationList = BinaryData.Load("SkinGroup" + i);
                for (int j = 0; j < informationList.Count; j++)
                {
                    HerosSkins[i][j].SetActive(informationList[j].IsEquipped);
                }
            }
        }
    }
}