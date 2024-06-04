using MonoSingleton;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities.SaveLoad;

namespace Manager
{
    public class MainMenuManager : MonoSingleton<MainMenuManager>
    {
        [SerializeField]private TextMeshProUGUI _levelText;
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
            _levelText.text = "LEVEL " + (SceneManager.GetActiveScene().buildIndex - 1);
            _settingsPanel = GameObject.FindWithTag("SettingsPanel");
        }

        #endregion

        public void SettingsPanel(bool active)
        {
            _settingsPanel.SetActive(active);
        }
    }
}