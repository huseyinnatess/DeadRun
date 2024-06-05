using UnityEngine.SceneManagement;
using Utilities.SaveLoad;

namespace Utilities
{
    public class PlayerLevel
    {
        /// <summary>
        /// Player'ın son level'ını ayarlar.
        /// </summary>
        /// <param name="sceneIndex">Aktif sahne index'i</param>
        public static void SetPlayerEndLevel(int sceneIndex)
        {
            if (sceneIndex == PlayerPrefsData.GetInt("EndLevel") &&
                sceneIndex < SceneManager.sceneCountInBuildSettings - 1)
            {
                PlayerPrefsData.SetInt("EndLevel", PlayerPrefsData.GetInt("EndLevel") + 1);
            }
        }
    }
}