using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utilities;
using Utilities.SaveLoad;
using Button = UnityEngine.UI.Button;

namespace Manager
{
    public class LevelSelectManager : MonoBehaviour
    {
        public List<GameObject> LockedButtons; // Henüz açılmamış level butonları
        public List<Button> Buttons; // Level butonları
        private int _currentLevel; // Mevcut level

        #region Awake
        private void Awake()
        {
            _currentLevel = PlayerPrefsData.GetInt("EndLevel") - 1;
            ButtonConfigure();
        }
        #endregion
        
        /// <summary>
        /// Level select sahnesinden çıkış butonu
        /// </summary>
        public void ExitButton()
        {
            LoadingSlider.Instance.StartLoad(PlayerPrefsData.GetInt("EndLevel"));
        }
        
        // Mevcut level'a göre butonların kilidini açıp her birine level indexi ataması yapıyor.
        private void ButtonConfigure()
        {
            int sceneIndex;
            for (int i = 0; i < Buttons.Count; i++)
            {
                if (i + 1 <= _currentLevel)
                {
                    LockedButtons[i].SetActive(false);
                    Buttons[i].GetComponentInChildren<Text>().text = (i + 1).ToString();
                    sceneIndex = i + 2;
                    Buttons[i].onClick.AddListener(delegate { SceneLoad(sceneIndex); });
                }
                else
                {
                    Buttons[i].interactable = false;
                }
            }
        }
        
        // Parametre olarak gelen sayıya göre sahne yükleme işlemi yapıyor.
        private void SceneLoad(int index)
        {
            LoadingSlider.Instance.StartLoad(index);
        }
    }
}