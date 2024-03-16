using GoogleAdmob.Interface;
using GoogleMobileAds.Api;
using UnityEngine;

namespace GoogleAdmob
{
    public class InterstitialAD : MonoBehaviour, IAdmob
    {
#if UNITY_EDITOR
        private const string _adUnitId = "ca-app-pub-3940256099942544/6300978111";
#endif

        private InterstitialAd _interstitialAd;
        private const int InterstitialFrequency = 10;
        private void Start()
        {
            MobileAds.Initialize(status => { });
        }
        public void CreateAd()
        {
            DestroyAd();
            var request = new AdRequest.Builder().Build();
            InterstitialAd.Load(_adUnitId, request, (ad, error) =>
            {
                if (ad == null || error != null)
                    return;
                _interstitialAd = ad;
            });
            EventsHandler();
        }

        public void ShowAd()
        {
            int interstitialCount = PlayerPrefs.GetInt(("Interstitial") + 1);
            PlayerPrefs.SetInt("Interstitial", interstitialCount);
            if (_interstitialAd == null || !_interstitialAd.CanShowAd())
                CreateAd();
            if (_interstitialAd != null && _interstitialAd.CanShowAd() && interstitialCount >= InterstitialFrequency)
            {
                _interstitialAd.Show();
                PlayerPrefs.SetInt("Interstitial", 0);
            }
        }

        public void DestroyAd()
        {
            if (_interstitialAd == null) return;
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }

        public void EventsHandler()
        {
            _interstitialAd.OnAdFullScreenContentClosed += CreateAd;
            _interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
            {
                CreateAd();
            };
        }
    }
}