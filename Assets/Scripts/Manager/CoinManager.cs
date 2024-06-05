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
        
        // Satın alma işlemi için yeterli coin olup olmadığını kontrol eder
        public bool ProcessPurchase(int price)
        {
            if (!CheckPurchase(price)) return false;
            SpendCoin(price);
            return (true);
        }
        
        // Coin kazanıldığı zaman coin miktarını güncelleyip kaydeder
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