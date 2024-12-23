﻿using UnityEngine;

namespace Utilities.SaveLoad
{
    public class PlayerPrefsData : MonoBehaviour
    {
        private void Awake()
        {
            DefaultData();
        }


        public static void SetInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
            PlayerPrefs.Save();
        }


        public static void SetFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
            PlayerPrefs.Save();
        }


        public static void SetString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
            PlayerPrefs.Save();
        }


        public static int GetInt(string key)
        {
            return PlayerPrefs.GetInt(key);
        }


        public static float GetFloat(string key)
        {
            return PlayerPrefs.GetFloat(key);
        }


        public static string GetString(string key)
        {
            return PlayerPrefs.GetString(key);
        }


        public static bool HasKey(string key)
        {
            return PlayerPrefs.HasKey(key);
        }


        public static void DeleteKey(string key)
        {
            if (HasKey(key))
                PlayerPrefs.DeleteKey(key);
        }


        public static void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }

        private void DefaultData()
        {
            if (HasKey("First")) return;
            SetInt("First", 1);
            SetInt("EndLevel", 2);
            SetInt("Coin", 10000);
            SetInt("Interstitial", 0);
            SetInt("ActiveHeroIndex", 0);
            SetInt("CurrentIndex", 0);
            SetInt("IsFirst", 0);
            SetInt("Score", 0);
            SetFloat("SoundSlider", .2f);
            SetFloat("FxSlider", 1f);
            SetInt("ComingSoonIsShow", 0);
            PlayerPrefs.Save();
        }
    }
}