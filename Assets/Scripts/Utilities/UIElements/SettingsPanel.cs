using MonoSingleton;
using UnityEngine;

namespace Utilities.UIElements
{
    public class SettingsPanel : MonoSingleton<SettingsPanel>
    {
        [SerializeField]private GameObject _settingsPanel; // Ayarlar paneli
        
        // False edilmeden önce slider'ları AudioSetting tarafından tag ile erişilmesi gerekiyor.
        // Bu yüzden awake içerisine alınmamalı.
        private void Start()
        {
            SettingPanel(false);
        }
        
        // Parametre olarak gelen değişkene göre panel'in aktifliğini ayarlıyor.
        public void SettingPanel(bool active)
        {
            _settingsPanel.SetActive(active);
        }
    }
}