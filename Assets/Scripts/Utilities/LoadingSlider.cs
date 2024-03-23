using System;
using System.Collections;
using MonoSingleton;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Utilities
{
    public class LoadingSlider : MonoSingleton<LoadingSlider>
    {
        private GameObject _loadingPanel;
        private Slider _loadingSlider;

        #region Awake

        private void Awake()
        {
            _loadingPanel = GameObject.FindWithTag("LoadingPanel");
            _loadingSlider = _loadingPanel.GetComponentInChildren<Slider>();
            _loadingPanel.SetActive(false);
        }

        #endregion
        

        public void StartLoad(int sceneIndex)
        {
            if (_loadingPanel != null)
                _loadingPanel.SetActive(true);
            StartCoroutine(LoadAsync(sceneIndex));
        }

        private IEnumerator LoadAsync(int sceneIndex)
        {
            AsyncOperation op = SceneManager.LoadSceneAsync(sceneIndex);
            while (!op.isDone)
            {
                float value = Mathf.Clamp01(op.progress / .9f);
                _loadingSlider.value = value;
                yield return null;
            }
        }
    }
}