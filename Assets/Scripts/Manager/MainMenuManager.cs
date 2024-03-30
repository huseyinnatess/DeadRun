using MonoSingleton;
using TMPro;
using UnityEngine;
using Utilities.SaveLoad;

namespace Manager
{
    public class MainMenuManager : MonoSingleton<MainMenuManager>
    {
        private TextMeshProUGUI _levelText;
        private GameObject _settingsPanel;

        #region Awake, Start, Get Functions

        private void Awake()
        {
            Time.timeScale = 1f;
            GetReferencesAwake();
        }

        private void Start()
        {
            _settingsPanel.SetActive(false);
        }

        private void GetReferencesAwake()
        {
            _levelText = GameObject.FindWithTag("LevelText").GetComponent<TextMeshProUGUI>();
            _levelText.text = "LEVEL " + (PlayerData.GetInt("EndLevel") - 2);
            _settingsPanel = GameObject.FindWithTag("SettingsPanel");
        }

        #endregion

        public void SettingsPanel(bool active)
        {
            _settingsPanel.SetActive(active);
        }
    }
}