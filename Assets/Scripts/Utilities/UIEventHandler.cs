using System;
using Manager;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities.UIElements;

namespace Utilities
{
    public class UIEventHandler : MonoBehaviour
    {
        public void NextLevelButton()
        {
            if (PlayerPrefs.GetInt("EndLevel") == SceneManager.sceneCountInBuildSettings) 
                PlayerPrefs.SetInt("EndLevel", PlayerPrefs.GetInt("EndLevel") - 1);
            LoadingSlider.Instance.StartLoad(PlayerPrefs.GetInt("EndLevel"));
            Time.timeScale = 1f;
        }

        public void RestartButton()
        {
            LoadingSlider.Instance.StartLoad(SceneManager.GetActiveScene().buildIndex);
            LevelPanelManager.Instance.MainMenuPanel(false);
            Time.timeScale = 1f;
        }

        public void MainMenuButton()
        {
            LoadingSlider.Instance.StartLoad(SceneManager.GetActiveScene().buildIndex);
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
           LevelPanelManager.Instance.MainMenuPanels.GetComponent<Animator>().SetTrigger("IsStartGame");
           LevelPanelManager.Instance.GamePanels.SetActive(true);
           LevelPanelManager.Instance.GamePanels.GetComponent<Animator>().SetTrigger("IsStartGame");
        } 
        public void StoreButton()
        {
            LoadingSlider.Instance.StartLoad(1);
        }
        public void LevelSelectButton()
        {
            LoadingSlider.Instance.StartLoad(0);
        }

        public void PauseAndContinueButton(bool active)
        {
            Time.timeScale = Convert.ToInt32(!active);
            LevelPanelManager.Instance.PausePanel(active);
        }
    }
}
