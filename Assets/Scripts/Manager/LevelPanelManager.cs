using System;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Manager
{
    public class LevelPanelManager : MonoBehaviour
    {
        private static GameObject _victoryPanel;
        private static GameObject _defeatPanel;
        private static GameObject _pausePanel;

        #region Awake Start

        private void Awake()
        {
            _victoryPanel = GameObject.FindWithTag("VictoryPanel");
            _defeatPanel = GameObject.FindWithTag("DefeatPanel");
            _pausePanel = GameObject.FindWithTag("PausePanel");
        }

        private void Start()
        {
            _pausePanel.SetActive(false);
            _victoryPanel.SetActive(false);
            _defeatPanel.SetActive(false);
        }

        #endregion
        
        public static void VictoryPanel(bool active)
        {
            _victoryPanel.SetActive(active);
        }

        public static void DefeatPanel(bool active)
        {
            _defeatPanel.SetActive(active);
        }

        public static void PausePanel(bool active)
        {
            _pausePanel.SetActive(active);
        }
    }
}