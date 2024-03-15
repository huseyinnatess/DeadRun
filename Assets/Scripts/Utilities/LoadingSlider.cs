using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Utilities
{
    public class LoadingSlider : MonoBehaviour
    {
        private GameObject _loadingPanel;
        private Slider _loadingSlider;
        public static LoadingSlider Instance;

        private void Awake()
        {
            if (!Instance)
                Instance = this;
            _loadingPanel = GameObject.FindWithTag("LoadingPanel");
            _loadingSlider = _loadingPanel.GetComponentInChildren<Slider>();
            _loadingPanel.SetActive(false);
        }

        public void StartLoad(int sceneIndex)
        {
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