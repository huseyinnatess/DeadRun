using Manager;
using UnityEngine;
using Utilities.UIElements;
using UnityEngine.SceneManagement;

namespace Utilities
{
    public class UIEventHandler : MonoBehaviour
    {
        private LoadingSlider _loadingSlider;
        private LevelPanelManager _levelPanelManager;

        private void Start()
        {
            _loadingSlider = LoadingSlider.Instance;
            _levelPanelManager = LevelPanelManager.Instance;
        }


        public void NextLevelButton()
        {
            int endLevel = PlayerPrefs.GetInt("EndLevel");

            int sceneIndex = SceneManager.GetActiveScene().buildIndex == endLevel
                ? endLevel
                : SceneManager.GetActiveScene().buildIndex + 1;

            _loadingSlider.StartLoad(sceneIndex);
            Time.timeScale = 1f;
        }


        public void RestartButton()
        {
            _loadingSlider.StartLoad(SceneManager.GetActiveScene().buildIndex);
            _levelPanelManager.MainMenuPanel(false);
            Time.timeScale = 1f;
        }


        public void MainMenuButton()
        {
            _loadingSlider.StartLoad(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1f;
        }


        public void SettingsButton(bool active)
        {
            SettingsPanel.Instance.SettingPanel(active);
        }


        public void StartGameButton()
        {
            GameManager.Instance.ActivateHandle(true);
            GameManager.HandelIsActive = true;
            GameManager.GameIsStart = true;
            _levelPanelManager.MainMenuPanels.GetComponent<Animator>().SetTrigger("IsStartGame");
            _levelPanelManager.GamePanels.SetActive(true);
            _levelPanelManager.GamePanels.GetComponent<Animator>().SetTrigger("IsStartGame");
            GameManager.ShowComingSoonMessage();
        }


        public void StoreButton()
        {
            _loadingSlider.StartLoad(1);
        }


        public void LevelSelectButton()
        {
            _loadingSlider.StartLoad(0);
        }


        public void PauseAndContinueButton(bool active)
        {
            _levelPanelManager.PausePanel(active);
        }

        public void CreditsPanel(bool active)
        {
            _levelPanelManager.CreditsPanel.SetActive(active);
        }
    }
}