using Manager.Store;
using UnityEngine;

namespace Utilities.Store.Skin
{
    public class SkinButtonsEventHandler : MonoBehaviour
    {
        public void ShowSkin(int group, int index)
        {
            SkinStoreManager.Instance.SkinObjectsMatrix[group][index].SetActive(true);
        }
    }
}