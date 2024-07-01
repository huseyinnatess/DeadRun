using UnityEngine;

namespace Utilities.UIElements
{
    public class CreditPanel : MonoBehaviour
    {
        public void CloseCreditsPanel()
        {
            gameObject.SetActive(false);
        }
    }
}