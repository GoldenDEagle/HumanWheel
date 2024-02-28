namespace Assets.Codebase.Infrastructure.ServicesManagment.Ads
{
    /// <summary>
    /// Managing ads.
    /// </summary>
    public interface IAdsService : IService
    {
        // Rewarded Ad
        public bool CheckIfRewardedIsAvailable();
        public void ShowRewarded();

        // Fullscreen Ad
        public bool CheckIfFullscreenIsAvailable();
        public void ShowFullscreen();

        /// <summary>
        /// Enables or disables ads.
        /// </summary>
        /// <param name="adsEnabled"></param>
        public void SetAdsStatus(bool adsEnabled);
    }
}
