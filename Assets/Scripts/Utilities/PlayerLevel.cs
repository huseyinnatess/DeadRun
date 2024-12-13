using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities.SaveLoad;

namespace Utilities
{
    public class PlayerLevel : MonoBehaviour
    {
        public static void SetPlayerEndLevel(int sceneIndex)
        {
            if (sceneIndex == PlayerPrefsData.GetInt("EndLevel") &&
                sceneIndex < SceneManager.sceneCountInBuildSettings - 1) // Son level ise
            {
                PlayerPrefsData.SetInt("EndLevel", PlayerPrefsData.GetInt("EndLevel") + 1);
            }

            if (PlayerPrefsData.GetInt("EndLevel") + 1 == SceneManager.sceneCountInBuildSettings
                && PlayerPrefsData.GetInt("ComingSoonIsShow") == 0)
            {
                PlayerPrefsData.SetInt("ComingSoonIsShow", 1);
            }
        }
    }
}