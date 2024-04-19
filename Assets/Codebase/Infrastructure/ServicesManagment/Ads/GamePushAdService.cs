using Assets.Codebase.Data.Audio;
using Assets.Codebase.Infrastructure.ServicesManagment.Audio;
using GamePush;
using UniRx;
using UnityEngine;

namespace Assets.Codebase.Infrastructure.ServicesManagment.Ads
{
    public class GamePushAdService : IAdsService
    {
        private bool _adsEnabled = true;
        public Subject<Unit> OnRewardedSuccess { get; private set; }
        public Subject<Unit> OnFullscreenClosed { get; private set; }
        public Subject<Unit> OnAdStarted { get; private set; }
        public Subject<Unit> OnAdEnded { get; private set; }

        private IAudioService _audio;

        public GamePushAdService(IAudioService audio)
        {
            OnAdStarted = new Subject<Unit>();
            OnAdEnded = new Subject<Unit>();
            OnRewardedSuccess = new Subject<Unit>();
            OnFullscreenClosed = new Subject<Unit>();
            _audio = audio;
            GP_Ads.OnRewardedReward += RewardedSuccess;
            GP_Ads.OnFullscreenClose += FullscreenClosed;
            GP_Game.OnPause += AdStarted;
            GP_Game.OnResume += AdEneded;
        }

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

            GP_Ads.ShowFullscreen();
        }

        public bool CheckIfRewardedIsAvailable()
        {
            return GP_Ads.IsRewardedAvailable();
        }

        public void ShowRewarded()
        {
            GP_Ads.ShowRewarded();
        }

        private void RewardedSuccess(string key)
        {
            OnRewardedSuccess?.OnNext(Unit.Default);
            _audio.PlaySfxSound(SoundId.None);
        }

        private void FullscreenClosed(bool correctly)
        {
            OnFullscreenClosed?.OnNext(Unit.Default);
        }

        private void AdStarted()
        {
            _audio.MuteAll();
            Time.timeScale = 0f;
            OnAdStarted?.OnNext(Unit.Default);
        }

        private void AdEneded()
        {
            _audio.UnmuteAll();
            Time.timeScale = 1f;
            OnAdEnded?.OnNext(Unit.Default);
        }

        public bool IsDeviceMobile()
        {
            return GP_Device.IsMobile();
        }
    }
}
