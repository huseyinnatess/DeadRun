using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities.SaveLoad;
using Utilities.UIElements;
using Button = UnityEngine.UI.Button;

namespace Manager
{
    public class LevelSelectManager : MonoBehaviour
    {
        public List<Button> Buttons;
        private List<Image> _lockImage;
        private int _currentLevel;

        #region Awake

        private void Awake()
        {
            _currentLevel = PlayerPrefsData.GetInt("EndLevel") - 1;
            GetLockImage();
            ButtonConfigure();
        }

        private void GetLockImage()
        {
            _lockImage = new List<Image>(Buttons.Count);
            for (int i = 0; i < Buttons.Count; i++)
            {
                _lockImage.Add(Buttons[i].transform.GetChild(0).GetComponent<Image>());
            }
        }

        #endregion


        public void ExitButton()
        {
            LoadingSlider.Instance.StartLoad(PlayerPrefsData.GetInt("EndLevel"));
        }

        private void ButtonConfigure()
        {
            for (int i = 0; i < Buttons.Count; i++)
            {
                if (i + 1 <= _currentLevel)
                {
                    _lockImage[i].enabled = false;
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