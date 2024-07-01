using System;
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

        /// <summary>
        /// Sonraki level sahnesini yükler.
        /// </summary>
        public void NextLevelButton()
        {
            int endLevel = PlayerPrefs.GetInt("EndLevel");
            
            int sceneIndex = SceneManager.GetActiveScene().buildIndex == endLevel
                ? endLevel : SceneManager.GetActiveScene().buildIndex + 1;
            
            _loadingSlider.StartLoad(sceneIndex);
            Time.timeScale = 1f;
        }
        
        /// <summary>
        /// Mevcut sahneyi yeniden yükler.
        /// </summary>
        public void RestartButton()
        {
            _loadingSlider.StartLoad(SceneManager.GetActiveScene().buildIndex);
            _levelPanelManager.MainMenuPanel(false);
            Time.timeScale = 1f;
        }
        
        /// <summary>
        /// Anamenü'ye dönmeyi sağlar.
        /// </summary>
        public void MainMenuButton()
        {
            _loadingSlider.StartLoad(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1f;
        }
        
        /// <summary>
        /// Ayarlar panelini aktif veya deaktif eder.
        /// </summary>
        /// <param name="active">Aktif etmek için true deaktif etmek için false</param>
        public void SettingsButton(bool active)
        {
            SettingsPanel.Instance.SettingPanel(active);
        }
        
        /// <summary>
        /// Oyunu başlatır.
        /// </summary>
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
        
        /// <summary>
        /// Market sahnesini yükler.
        /// </summary>
        public void StoreButton()
        {
            _loadingSlider.StartLoad(1);
        }
        
        /// <summary>
        /// Level select sahnesini yükler.
        /// </summary>
        public void LevelSelectButton()
        {
            _loadingSlider.StartLoad(0);
        }
        
        /// <summary>
        /// Duraklatma panelini aktif veya deaktif edip oyunu durdurur.
        /// </summary>
        /// <param name="active">Paneli açıp oyunu durdurmak için true aksi halde false</param>
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
