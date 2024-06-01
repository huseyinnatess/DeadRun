﻿using System;
using System.Collections.Generic;
using Controller;
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
    public class GameManager : MonoBehaviour
    {
        private Battlefield _battlefield;
        private bool _checkWarStatus;
        private GameObject _handel;
        public static int IsSetActiveHandle;
        
        private Slider _slider;
        private GameObject _finishPosition;
        private GameObject _startPosition;
        private TextMeshProUGUI _levelText;

        public List<GameObject> Heros;
        public List<GameObject> HatSkins;
        public List<GameObject> SwordSkins;
        public List<GameObject> ArmorSkins;
        public List<List<GameObject>> HerosSkins;

        #region Awake Start Get Functions

        private void Awake()
        {
            Time.timeScale = 1f;
            IsSetActiveHandle = 0;
            GetReferences();
            ActivateHero();
            InitializeSkins();
            ActivateHeroSkins();
        }

        private void GetReferences()
        {
            _slider = GetComponentInChildren<Slider>();
            _levelText = GameObject.FindWithTag("LevelText").GetComponent<TextMeshProUGUI>();
            _levelText.text = "LEVEL " + (SceneManager.GetActiveScene().buildIndex - 2);
            _handel = GameObject.FindWithTag("Handel");
        }

        private void Start()
        {
            GetReferencesStart();
        }

        private void GetReferencesStart()
        {
            _finishPosition = GameObject.FindWithTag("Battlefield");
            _battlefield = _finishPosition.GetComponent<Battlefield>();
            _startPosition = GameObject.FindWithTag("Character");
            _slider.maxValue = Vector3.Distance(_finishPosition.transform.position, _startPosition.transform.position);
            _checkWarStatus = false;
        }
        
        #endregion

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && IsSetActiveHandle != 1)
            {
                _handel.SetActive(false);
                IsSetActiveHandle = 1;
            }
        }
        private void LateUpdate()
        {
            CheckWarResult();
            SliderUpdate();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void CheckWarResult()
        {
            if ((EnemyController.IsCanAttack && !_checkWarStatus &&
                 (AgentPools.Instance.AgentCount == 0 || EnemyController.EnemyAgentCount == 0)) ||
                (AgentPools.Instance.AgentCount == 0))
            {
                _checkWarStatus = true;
                _battlefield.WarResult(EnemyController.EnemyAgentCount);
            }
        }

        private void SliderUpdate()
        {
            if (Math.Abs(_slider.value - _slider.maxValue) < .2f) return;
            _slider.value = _slider.maxValue -
                Vector3.Distance(_startPosition.transform.position, _finishPosition.transform.position) + 0.58f;
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