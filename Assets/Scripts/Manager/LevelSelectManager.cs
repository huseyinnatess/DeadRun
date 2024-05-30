using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
            _currentLevel = PlayerPrefsData.GetInt("EndLevel") - 2;

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
                    int sceneIndex = i + 3;
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