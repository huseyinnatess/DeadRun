using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utilities.UIElements
{
    public class MenuLevelText : MonoBehaviour
    {
       [SerializeField] private TextMeshProUGUI _levelText; // AnaMenü level text

        private void Awake()
        {
            _levelText.text = "LEVEL " + (SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}