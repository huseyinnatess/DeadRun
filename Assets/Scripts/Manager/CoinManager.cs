using MonoSingleton;
using TMPro;
using UnityEngine;
using Utilities.SaveLoad;

namespace Manager
{
    public class CoinManager : MonoSingleton<CoinManager>
    {
        private TextMeshProUGUI _coinText;
        private int _coin;

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


        public bool ProcessPurchase(int price)
        {
            if (!CheckPurchase(price)) return false;
            SpendCoin(price);
            return (true);
        }


        public void EarnCoin(int amount)
        {
            _coin += amount;
            _coinText.text = _coin.ToString();
            SaveCoin();
        }

        private void SpendCoin(int amount)
        {
            _coin -= amount;
            _coinText.text = _coin.ToString();
            SaveCoin();
        }

        private bool CheckPurchase(int price)
        {
            return _coin >= price;
        }

        private void SaveCoin()
        {
            PlayerPrefsData.SetInt("Coin", _coin);
        }
    }
}