using System.Collections.Generic;
using Controller;
using Manager.Audio.Utilities;
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
        private List<List<GameObject>> _herosSkins; // Tüm skinlerin tutulduğu çift boyutlu liste

        public static bool GameIsStart; // Karakter koşmaya başlayarak oyun başladı mı? kontrolü.

        
        #region Awake, Start, Get, Set Functions

        private void Awake()
        {
            Time.timeScale = 1f;
            GetReferences();
            SetReferences();
            ActivateHero();
            InitializeSkins();
            ActivateHeroSkins();
        }

        private void GetReferences()
        {
            _handel = GameObject.FindWithTag("Handel");
            HandelIsActive = false;
            GameIsStart = false;
        }

        private void SetReferences()
        {
            _sliderLevelText.text = "LEVEL " + (SceneManager.GetActiveScene().buildIndex - 1);
            ActivateHandle(false);
        }

        private void Start()
        {
            FxSounds.Instance.CharacterRunFx.Stop();
        }

        #endregion

        #region Update

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0) || !HandelIsActive) return;
            ActivateHandle(false);
            HandelIsActive = false;
            CharacterControl.Instance.enabled = true;
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
            _herosSkins = new List<List<GameObject>> { HatSkins, SwordSkins, ArmorSkins };
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
            List<StoreInformations> informationList;
            for (int i = 0; i < InitializeSkinInfos.ListCount; i++)
            {
                informationList = BinaryData.Load("SkinGroup" + i);
                if (informationList is null) continue;
                for (int j = 0; j < informationList.Count; j++)
                {
                    _herosSkins[i][j].SetActive(informationList[j].IsEquipped);
                }
            }
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            // LevelPanelManager.Instance.PausePanel(!hasFocus);
        }
    }
}