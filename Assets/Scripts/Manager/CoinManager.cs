using MonoSingleton;
using TMPro;
using UnityEngine;
using Utilities.SaveLoad;

namespace Manager
{
    public class CoinManager : MonoSingleton<CoinManager>
    {
        private TextMeshProUGUI _coinText; // Coin'in yazıldığı text
        private int _coin; // Toplam coin miktarı
        
        #region Awake, Get Functions
        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            _coinText = GameObject.FindWithTag("Coin").GetComponent<TextMeshProUGUI>();
            _coin = PlayerPrefsData.GetInt("Coin");
            _coinText.text = _coin.ToString();
        }
        #endregion
        
        /// <summary>
        /// Satın alma işlemi için yeterli coin olup olmadığını kontrol eder
        /// </summary>
        /// <param name="price"> Ürünün fiyatı </param>
        /// <returns> true veya false dönüş yapar. </returns>
        public bool ProcessPurchase(int price)
        {
            if (!CheckPurchase(price)) return false;
            SpendCoin(price);
            return (true);
        }
        
        /// <summary>
        /// Coin kazanıldığı zaman coin miktarını güncelleyip kaydeder
        /// </summary>
        /// <param name="amount"> Kazanılan coin miktarı </param>
        public void EarnCoin(int amount)
        {
            _coin += amount;
            _coinText.text = _coin.ToString();
            SaveCoin();
        }
        
        // Coin harcandığı zaman coin miktarını güncelleyip kaydeder
        private void SpendCoin(int amount)
        {
            _coin -= amount;
            _coinText.text = _coin.ToString();
            SaveCoin();
        }
        
        // Satın alma işlemi için yeterli coin olup olmadığını kontrol eder
        private bool CheckPurchase(int price)
        {
            return _coin >= price;
        }
        
        // Coin miktarını kaydeder
        private void SaveCoin()
        {
            PlayerPrefsData.SetInt("Coin", _coin);
        }
    }
}