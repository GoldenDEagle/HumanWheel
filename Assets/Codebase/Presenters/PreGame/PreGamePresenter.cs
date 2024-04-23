using Assets.Codebase.Infrastructure.ServicesManagment.Ads;
using Assets.Codebase.Infrastructure.ServicesManagment;
using Assets.Codebase.Models.Gameplay.Data;
using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Views.Base;
using System;
using UniRx;
using Cysharp.Threading.Tasks;
using GamePush;

namespace Assets.Codebase.Presenters.PreGame
{
    public class PreGamePresenter : BasePresenter, IPreGamePresenter
    {
        public ReactiveProperty<string> TotalCoinsString { get; private set; }
        public ReactiveProperty<string> CurrentLevelString { get; private set; }
        public ReactiveProperty<bool> WatchAdButtonActiveState { get; private set; }

        private IDisposable _rewardedSubscription;

        private int _coinAnimationStep = 25;
        private int _buyHumanPrice = 50;
        private int _adRewardedCoins = 50;

        public PreGamePresenter()
        {
            CorrespondingViewId = ViewId.PreGameView;

            CurrentLevelString = new ReactiveProperty<string>();
            TotalCoinsString = new ReactiveProperty<string>();
            WatchAdButtonActiveState = new ReactiveProperty<bool>();
        }

        public override void CreateView()
        {
            base.CreateView();
            WatchAdButtonActiveState.Value = ServiceLocator.Container.Single<IAdsService>().CheckIfRewardedIsAvailable();
            TotalCoinsString.Value = ProgressModel.SessionProgress.TotalCoins.Value.ToString();
        }

        protected override void SubscribeToModelChanges()
        {
            base.SubscribeToModelChanges();
            ProgressModel.SessionProgress.CurrentLevel.Subscribe(value => CurrentLevelString.Value = value.ToString()).AddTo(CompositeDisposable);
        }

        public void StartGameClicked()
        {
            GameplayModel.ChangeGameState(GameState.Game);
            GameplayModel.ActivateView(ViewId.IngameView);
        }

        public void BuyHumanClicked()
        {
            if (ProgressModel.SessionProgress.TotalCoins.Value < _buyHumanPrice) return;

            ProgressModel.ModifyTotalCoins(-_buyHumanPrice);
            TotalCoinsString.Value = ProgressModel.SessionProgress.TotalCoins.Value.ToString();

            GameplayModel.AddHumanToWheel();
        }

        public void WatchAdForCoinsClicked()
        {
            var adService = ServiceLocator.Container.Single<IAdsService>();

            if (!adService.CheckIfRewardedIsAvailable()) return;

            _rewardedSubscription = adService.OnRewardedSuccess.Subscribe(_ => OnRewardGranted()).AddTo(CompositeDisposable);
            adService.ShowRewarded();
        }

        public void LeaderboardButtonClicked()
        {
            GP_Leaderboard.Open();
        }



        private void OnRewardGranted()
        {
            CompositeDisposable.Remove(_rewardedSubscription);
            PrettyCoinIncrease(ProgressModel.SessionProgress.TotalCoins.Value, _adRewardedCoins);
            ProgressModel.SaveProgress();
        }

        private void PrettyCoinIncrease(int initialCoinValue, int coinDelta)
        {
            var endCoins = initialCoinValue + coinDelta;
            AnimateCoinIncrease(initialCoinValue, endCoins).Forget();
            ProgressModel.ModifyTotalCoins(coinDelta);
        }

        private async UniTaskVoid AnimateCoinIncrease(int startingValue, int endingValue)
        {
            int value = startingValue;
            TotalCoinsString.Value = value.ToString();

            while (value < endingValue)
            {
                await UniTask.Delay(_coinAnimationStep);
                value++;
                TotalCoinsString.Value = value.ToString();
            }
        }
    }
}
