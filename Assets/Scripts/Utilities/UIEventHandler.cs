using System;
using Manager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utilities
{
    public class UIEventHandler : MonoBehaviour
    {
        public void NextLevel()
        {
            LoadingSlider.Instance.StartLoad(PlayerPrefs.GetInt("EndLevel"));
            Time.timeScale = 1f;
        }

        public void Restart()
        {
            LoadingSlider.Instance.StartLoad(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1f;
        }

        public void MainMenu()
        {
            LoadingSlider.Instance.StartLoad(0);
            Time.timeScale = 1f;
        }

        public void Settings(bool active)
        {
            MainMenuManager.Instance.SettingsPanel(active);
        }
        
        public void StartGame()
        {
            LoadingSlider.Instance.StartLoad(PlayerPrefs.GetInt("EndLevel"));
        }
        public void LevelSelect(int index)
        {
            LoadingSlider.Instance.StartLoad(index);
        }

        public void PauseAndContinue(bool active)
        {
            Time.timeScale = Convert.ToInt32(!active);
            LevelPanelManager.PausePanel(active);
        }
    }
}
