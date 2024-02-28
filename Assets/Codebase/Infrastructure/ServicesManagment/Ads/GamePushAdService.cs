using GamePush;
using UnityEngine;

namespace Assets.Codebase.Infrastructure.ServicesManagment.Ads
{
    public class GamePushAdService : IAdsService
    {
        private bool _adsEnabled = true;

        public void SetAdsStatus(bool adsEnabled)
        {
            _adsEnabled = adsEnabled;
        }

        public bool CheckIfFullscreenIsAvailable()
        {
            return GP_Ads.IsFullscreenAvailable();
        }

        public void ShowFullscreen()
        {
            if (!_adsEnabled)
            {
                Debug.Log("Ads are disabled!");
                return;
            }

            if (CheckIfFullscreenIsAvailable())
            {
                GP_Ads.ShowFullscreen();
            }
        }

        public bool CheckIfRewardedIsAvailable()
        {
            return GP_Ads.IsRewardedAvailable();
        }

        public void ShowRewarded()
        {
            if (CheckIfRewardedIsAvailable())
            {
                GP_Ads.ShowRewarded();
            }
        }
    }
}
