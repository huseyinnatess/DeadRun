using System;
using Controller;
using ObjectPools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        private Battlefield _battlefield;
        private bool _checkWarStatus;

        private Slider _slider;
        private GameObject _finishPosition;
        private GameObject _startPosition;
        private TextMeshProUGUI _levelText;

        private void Awake()
        {
            Time.timeScale = 1f;
            _slider = GetComponentInChildren<Slider>();
            _levelText = GameObject.FindWithTag("LevelText").GetComponent<TextMeshProUGUI>();
            _levelText.text = "LEVEL " + (PlayerData.GetInt("EndLevel") - 1);
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

        private void LateUpdate()
        {
            CheckWarResult();
            SliderUpdate();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void CheckWarResult()
        {
            if ((EnemyController.IsCanAttack && !_checkWarStatus &&
                 (AgentPools.CharacterCount == 0 || EnemyController.EnemyAgentCount == 0)) ||
                 (AgentPools.CharacterCount == 0))
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
    }
}