using System;
using Manager.Audio;
using MonoSingleton;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.UIElements
{
    public class SettingsPanel : MonoSingleton<SettingsPanel>
    {
        public GameObject SettingPanell; // Ayarlar paneli
        
        private Slider[] _settingSliders; // Ayarlar panelindeki slider'ların tutulduğu array.
        
        #region Awake, Get Functions
        
        private void Awake()
        {
            GetReferences();
        }
        
        private void GetReferences()
        {
            _settingSliders = SettingPanell.GetComponentsInChildren<Slider>();
        }

        private void Start()
        {
            SetSlidersValue();
        }

        #endregion
        
        // Ayarlar panelinin Sound ve Fx sliderlarını günceller.
        private void SetSlidersValue()
        {
            _settingSliders[0].value = AudioManager.GetSoundSliderValue();
            _settingSliders[1].value = AudioManager.GetFxSliderValue();
        }
        
        /// <summary>
        /// Parametre olarak gelen değişkene göre panel'in aktifliğini ayarlar.
        /// </summary>
        /// <param name="active">Aktiflik durumu</param>
        public void SettingPanel(bool active) => SettingPanell.SetActive(active);
    }
}