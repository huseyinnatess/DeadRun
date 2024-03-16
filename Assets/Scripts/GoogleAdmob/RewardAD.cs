using GoogleAdmob.Interface;
using GoogleMobileAds.Api;
using UnityEngine;

namespace GoogleAdmob
{
    public class RewardAD : MonoBehaviour, IAdmob
    {
#if  UNITY_EDITOR
        private const string _adUnitId = "ca-app-pub-3940256099942544/5224354917";
#endif
        private RewardedAd _rewardedAd;
        public void CreateAd()
        {
            DestroyAd();
            var request = new AdRequest.Builder().Build();
            RewardedAd.Load(_adUnitId, request, (ad, error) =>
            {
                if (error != null || ad == null)
                    return;
                _rewardedAd = ad;
            });
            EventsHandler();
        }

        public void ShowAd()
        {
            if (_rewardedAd == null || !_rewardedAd.CanShowAd())
                CreateAd();
            if (_rewardedAd != null && _rewardedAd.CanShowAd())
            {
                _rewardedAd.Show(reward =>
                {
                    double amount = reward.Amount;
                    string type = reward.Type;
                    Debug.Log(type + ": " + amount + " Kazanıldı");
                });
            }
        }

        public void DestroyAd()
        {
            if (_rewardedAd == null) return;
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }

        public void EventsHandler()
        {
            _rewardedAd.OnAdFullScreenContentClosed += CreateAd;
            _rewardedAd.OnAdFullScreenContentFailed += (AdError error) =>
            {
                CreateAd();
            };
        }
    }
}