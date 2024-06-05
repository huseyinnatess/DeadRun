using System;
using System.Collections.Generic;
using Controller;
using MonoSingleton;
using ObjectPools;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utilities;
using Utilities.SaveLoad;
using Utilities.Store;
using Utilities.Store.Skin;

namespace Manager
{
    public class GameManager : MonoSingleton<GameManager>
    {
        private bool _checkWarStatus; // Savaş sonucunun döngü içerisinde tekrar tekrar kontrol edilmemesini sağlar
        private GameObject _handel; // Oyundaki sağa sola giden el nesnesi
        public static bool HandelIsActive; // Elin aktif olup olmadığını kontrol eder
        
       [SerializeField] private TextMeshProUGUI _sliderLevelText; // Game panel'deki level

        public List<GameObject> Heros; // Karakterlerin tutulduğu liste
        public List<GameObject> HatSkins; // Şapkaların listesi
        public List<GameObject> SwordSkins; // Kılıçların listesi
        public List<GameObject> ArmorSkins; // Zırhların listesi
        public List<List<GameObject>> HerosSkins; // Tüm skinlerin tutulduğu çift boyutlu liste

        #region Awake Start Get Functions
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

        private void Start()
        {
            GetReferencesStart();
        }

        private void GetReferencesStart()
        {
            _checkWarStatus = false;
        }
        
        #endregion

        #region Update, LateUpdate

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0) || !HandelIsActive) return;
            ActivateHandle(false);
            CharacterControl.Instance.enabled = true;
            HandelIsActive = false;
        }
        
        private void LateUpdate()
        {
            CheckWarResult();
        }
        #endregion
        
        // Update
        // Paramtetre olarak gelen state göre handle aktifliğini ayarlar
        public void ActivateHandle(bool state) => _handel.SetActive(state);

        // Savaş sonucunu kontrol eder.
        private void CheckWarResult()
        {
            if ((EnemyController.IsCanAttack && !_checkWarStatus &&
                 (AgentPools.Instance.AgentCount == 0 || EnemyController.EnemyAgentCount == 0)) ||
                AgentPools.Instance.AgentCount == 0)
            {
                _checkWarStatus = true;
                Battlefield.Instance.WarResult(AgentPools.Instance.AgentCount);
            }
        }

        private void InitializeSkins()
        {
            HerosSkins = new List<List<GameObject>>(3);
            HerosSkins.Add(HatSkins);
            HerosSkins.Add(SwordSkins);
            HerosSkins.Add(ArmorSkins);
        }
        private void ActivateHero()
        {
            for (int i = 0; i < Heros.Count; i++)
            {
                Heros[i].SetActive(false);
            }

            Heros[PlayerPrefsData.GetInt("ActiveHeroIndex")].SetActive(true);
        }

        private void ActivateHeroSkins()
        {
            for (int i = 0; i < InitializeSkinInfos.ListCount; i++)
            {
                List<StoreInformations> informationList = BinaryData.Load("SkinGroup" + i);
                for (int j = 0; j < informationList.Count; j++)
                {
                    if (informationList[j].IsEquipped)
                    {
                        HerosSkins[i][j].SetActive(true);
                    }
                    else
                    {
                        HerosSkins[i][j].SetActive(false);
                    }
                }
            }
        }
    }
}