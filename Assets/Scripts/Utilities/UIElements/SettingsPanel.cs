using System;
using Manager.Audio;
using MonoSingleton;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.UIElements
{
    public class SettingsPanel : MonoSingleton<SettingsPanel>
    {
        public GameObject SettingPanell;
        private Slider[] _settingSliders;

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

        private void SetSlidersValue()
        {
            _settingSliders[0].value = AudioManager.GetSoundSliderValue();
            _settingSliders[1].value = AudioManager.GetFxSliderValue();
        }


        public void SettingPanel(bool active) => SettingPanell.SetActive(active);
    }
}