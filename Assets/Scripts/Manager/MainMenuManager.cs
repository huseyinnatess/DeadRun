using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utilities;

namespace Manager
{
    public class MainMenuManager : MonoBehaviour
    {
        private TextMeshProUGUI _levelText;
        private GameObject _settingsPanel;
        public static MainMenuManager Instance;
        private void Awake()
        {
            Time.timeScale = 1f;
            Singleton();
            GetReferencesAwake();
        }

        private void Start()
        {
            _settingsPanel.SetActive(false);
        }

        private void GetReferencesAwake()
        {
            _levelText = GameObject.FindWithTag("LevelText").GetComponent<TextMeshProUGUI>();
            _levelText.text = "LEVEL " + (PlayerData.GetInt("EndLevel") - 1);
            _settingsPanel = GameObject.FindWithTag("SettingsPanel");
        }

        private void Singleton()
        {
            if (!Instance)
                Instance = this;
        }

        public void SettingsPanel(bool active)
        {
            _settingsPanel.SetActive(active);
        }
    }
}