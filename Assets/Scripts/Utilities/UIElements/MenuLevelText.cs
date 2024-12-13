using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utilities.UIElements
{
    public class MenuLevelText : MonoBehaviour
    {
       [SerializeField] private TextMeshProUGUI levelText;
        private void Awake()
        {
            levelText.text = "LEVEL " + (SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}