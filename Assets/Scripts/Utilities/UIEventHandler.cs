using System;
using Manager;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities.SaveLoad;
using Utilities.UIElements;

namespace Utilities
{
    public class UIEventHandler : MonoBehaviour
    {
        /// <summary>
        /// Sonraki level sahnesini yükler.
        /// </summary>
        public void NextLevelButton()
        {
            LoadingSlider.Instance.StartLoad(PlayerPrefs.GetInt("EndLevel"));
            Time.timeScale = 1f;
        }
        
        /// <summary>
        /// Mevcut sahneyi yeniden yükler.
        /// </summary>
        public void RestartButton()
        {
            LoadingSlider.Instance.StartLoad(SceneManager.GetActiveScene().buildIndex);
            LevelPanelManager.Instance.MainMenuPanel(false);
            Time.timeScale = 1f;
        }
        
        /// <summary>
        /// Anamenü'ye dönmeyi sağlar.
        /// </summary>
        public void MainMenuButton()
        {
            LoadingSlider.Instance.StartLoad(SceneManager.GetActiveScene().buildIndex);
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
           LevelPanelManager.Instance.MainMenuPanels.GetComponent<Animator>().SetTrigger("IsStartGame");
           LevelPanelManager.Instance.GamePanels.SetActive(true);
           LevelPanelManager.Instance.GamePanels.GetComponent<Animator>().SetTrigger("IsStartGame");
        }
        
        /// <summary>
        /// Market sahnesini yükler.
        /// </summary>
        public void StoreButton()
        {
            LoadingSlider.Instance.StartLoad(1);
        }
        
        /// <summary>
        /// Level select sahnesini yükler.
        /// </summary>
        public void LevelSelectButton()
        {
            LoadingSlider.Instance.StartLoad(0);
        }
        
        /// <summary>
        /// Duraklatma panelini aktif veya deaktif edip oyunu durdurur.
        /// </summary>
        /// <param name="active">Paneli açıp oyunu durdurmak için true aksi halde false</param>
        public void PauseAndContinueButton(bool active)
        {
            Time.timeScale = Convert.ToInt32(!active);
            LevelPanelManager.Instance.PausePanel(active);
        }
    }
}
