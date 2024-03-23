using System;
using Manager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utilities
{
    public class UIEventHandler : MonoBehaviour
    {
        public void NextLevelButton()
        {
            LoadingSlider.Instance.StartLoad(PlayerPrefs.GetInt("EndLevel"));
            Time.timeScale = 1f;
        }

        public void RestartButton()
        {
            LoadingSlider.Instance.StartLoad(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1f;
        }

        public void MainMenuButton()
        {
            LoadingSlider.Instance.StartLoad(0);
            Time.timeScale = 1f;
        }

        public void SettingsButton(bool active)
        {
            MainMenuManager.Instance.SettingsPanel(active);
        }
        
        public void StartGameButton()
        {
            LoadingSlider.Instance.StartLoad(PlayerPrefs.GetInt("EndLevel"));
        } 
        public void MarketButton()
        {
            LoadingSlider.Instance.StartLoad(2);
        }
        public void LevelSelectButton()
        {
            LoadingSlider.Instance.StartLoad(1);
        }

        public void PauseAndContinueButton(bool active)
        {
            Time.timeScale = Convert.ToInt32(!active);
            LevelPanelManager.Instance.PausePanel(active);
        }
    }
}
