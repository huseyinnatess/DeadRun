using System.Collections;
using MonoSingleton;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Utilities.UIElements
{
    public class LoadingSlider : MonoSingleton<LoadingSlider>
    {
        [SerializeField] private GameObject _loadingPanel;
        [SerializeField] private Slider _loadingSlider;


        public void StartLoad(int sceneIndex)
        {
            _loadingPanel.SetActive(true);
            StartCoroutine(LoadAsync(sceneIndex));
        }

        private IEnumerator LoadAsync(int sceneIndex)
        {
            AsyncOperation op = SceneManager.LoadSceneAsync(sceneIndex);
            while (op is not null && !op.isDone)
            {
                float value = Mathf.Clamp01(op.progress / .9f);
                _loadingSlider.value = value;
                yield return null;
            }
        }
    }
}