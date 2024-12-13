using System.Collections.Generic;
using Controller;
using Controller.Utilities;
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
        private GameObject _handel;
        public static bool HandelIsActive;
        [SerializeField] private TextMeshProUGUI _sliderLevelText;
        public List<GameObject> Heros;
        public List<GameObject> HatSkins;
        public List<GameObject> SwordSkins;
        public List<GameObject> ArmorSkins;
        private List<List<GameObject>> _herosSkins;
        public static bool GameIsStart;

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

        public static void ShowComingSoonMessage()
        {
            if (PlayerPrefsData.GetInt("ComingSoonIsShow") != 1) return;
            Warning.Instance.SetComingSoonMessages();
            Warning.Instance.StartWriteInformation(true);
            PlayerPrefsData.SetInt("ComingSoonIsShow", 2);
        }


        public void ActivateHandle(bool state) => _handel.SetActive(state);
        private void InitializeSkins() => _herosSkins = new List<List<GameObject>> { HatSkins, SwordSkins, ArmorSkins };

        private void ActivateHero()
        {
            int activeHeroIndex = PlayerPrefsData.GetInt("ActiveHeroIndex");
            for (int i = 0; i < Heros.Count; i++)
            {
                Heros[i].SetActive(i == activeHeroIndex);
            }
        }

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
        }
    }
}