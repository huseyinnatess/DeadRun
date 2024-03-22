using System.Collections.Generic;
using Controller;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Utilities;
using Utilities.SaveLoad;
using Button = UnityEngine.UI.Button;

namespace Manager
{
    public class LevelSelectManager : MonoBehaviour
    {
        public List<GameObject> ButtonsLock;
        public List<Button> Buttons;
        private int _currentLevel;

        #region Awake

        private void Awake()
        {
            _currentLevel = PlayerData.GetInt("EndLevel") - 1;

            ButtonConfigure();
        }

        #endregion
        
        public void ExitButton()
        {
            LoadingSlider.Instance.StartLoad(0);
        }

        private void ButtonConfigure()
        {
            for (int i = 0; i < Buttons.Count; i++)
            {
                if (i + 1 <= _currentLevel)
                {
                    ButtonsLock[i].SetActive(false);
                    Buttons[i].GetComponentInChildren<Text>().text = (i + 1).ToString();
                    int sceneIndex = i + 2;
                    Buttons[i].onClick.AddListener(delegate { SceneLoad(sceneIndex); });
                }
                else
                {
                    Buttons[i].interactable = false;
                }
            }
        }

        private void SceneLoad(int index)
        {
            LoadingSlider.Instance.StartLoad(index);
        }
    }
}