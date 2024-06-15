using UnityEngine;

namespace Utilities.SaveLoad
{
    public class PlayerPrefsData : MonoBehaviour
    {
        private void Awake()
        {
            DefaultData();
        }
        /// <summary>
        /// Veri kayı etme işlemi yapar.
        /// </summary>
        /// <param name="key"> Kayıt edilecek verinin ismi.</param>
        /// <param name="value">Kayıt edilecek veri.</param>
        public static void SetInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
            PlayerPrefs.Save();
        }
        
        /// <summary>
        /// Veri kayı etme işlemi yapar.
        /// </summary>
        /// <param name="key"> Kayıt edilecek verinin ismi.</param>
        /// <param name="value">Kayıt edilecek veri.</param>
        public static void SetFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
            PlayerPrefs.Save();
        }
        
        /// <summary>
        /// Veri kayı etme işlemi yapar.
        /// </summary>
        /// <param name="key"> Kayıt edilecek verinin ismi.</param>
        /// <param name="value">Kayıt edilecek veri.</param>
        public static void SetString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
            PlayerPrefs.Save();
        }
        
        /// <summary>
        /// Kaydedilmiş veriyi alma işlemi yapar.
        /// </summary>
        /// <param name="key">Kaydedilen verinin ismi.</param>
        /// <returns>Kayıtlı veri var ise veriyi yok ise 0 döner.</returns>
        public static int GetInt(string key)
        {
           return PlayerPrefs.GetInt(key);
        }
        
        /// <summary>
        /// Kaydedilmiş veriyi alma işlemi yapar.
        /// </summary>
        /// <param name="key">Kaydedilen verinin ismi.</param>
        /// <returns>Kayıtlı veri var ise veriyi yok ise 0 döner.</returns>
        public static float GetFloat(string key)
        {
            return PlayerPrefs.GetFloat(key);
        }
        
        /// <summary>
        /// Kaydedilmiş veriyi alma işlemi yapar.
        /// </summary>
        /// <param name="key">Kaydedilen verinin ismi.</param>
        /// <returns>Kayıtlı veri var ise veriyi yok ise 0 döner.</returns>
        public static string GetString(string key)
        {
            return PlayerPrefs.GetString(key);
        }
        
        /// <summary>
        /// Verinin varlığını kontrol eder.
        /// </summary>
        /// <param name="key">Kontrol edilecek verinin ismi.</param>
        /// <returns>Veri var ise true yoksa false döner.</returns>
        public static bool HasKey(string key)
        {
            return PlayerPrefs.HasKey(key);
        }
        
        /// <summary>
        /// Veri silme işlemi yapar.
        /// </summary>
        /// <param name="key">Silinecek verinin ismi.</param>
        public static void DeleteKey(string key)
        {
            if (HasKey(key))
                PlayerPrefs.DeleteKey(key);
        }
        
        /// <summary>
        /// Tüm kayıtlı verileri siler.
        /// </summary>
        public static void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }
        
        // Oyun başlangıcında oluşturulup default değerlerin ataması yapılıyor.
        private void DefaultData()
        {
            if (HasKey("First")) return;
            SetInt("First", 1);
            SetInt("EndLevel", 2);
            SetInt("Coin", 10000);
            SetInt("Score", 0);
            SetFloat("SoundSlider", .2f);
            SetFloat("FxSlider", 1f);
            SetInt("Interstitial", 0);
            SetInt("ActiveHeroIndex", 0);
            SetInt("CurrentIndex", 0);
            SetInt("IsFirst", 0);
            PlayerPrefs.Save();
        }
    } 
}