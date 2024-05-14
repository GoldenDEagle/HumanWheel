using Assets.Codebase.Infrastructure.ServicesManagment.Ads;
using Assets.Codebase.Infrastructure.ServicesManagment;
using Assets.Codebase.Models.Gameplay.Data;
using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Utils.Values;
using Assets.Codebase.Views.Base;
using Cysharp.Threading.Tasks;
using System;
using UniRx;
using Assets.Codebase.Infrastructure.ServicesManagment.Audio;
using Assets.Codebase.Data.Audio;
using Assets.Codebase.Infrastructure.ServicesManagment.Localization;

namespace Assets.Codebase.Presenters.EndGame
{
    public class EndgamePresenter : BasePresenter, IEndgamePresenter
    {
        public ReactiveProperty<string> ClearedLevelString { get; private set; }
        public ReactiveProperty<string> TotalCoinsString { get; private set; }
        public ReactiveProperty<string> CollectedCoinsString { get; private set; }
        public ReactiveProperty<bool> DoubleRewardButtonActiveState { get; private set; }

        private const string LEVELCLEARED_KEY = "endgame_cleared";

        private IDisposable _rewardedSubscription;

        private int _coinAnimationStep = 50;

        public EndgamePresenter()
        {
            CorrespondingViewId = ViewId.EndgameView;

            ClearedLevelString = new ReactiveProperty<string>();
            TotalCoinsString = new ReactiveProperty<string>();
            CollectedCoinsString = new ReactiveProperty<string>();
            DoubleRewardButtonActiveState = new ReactiveProperty<bool>();
        }

        public override void CreateView()
        {
            base.CreateView();
            //ServiceLocator.Container.Single<IAudioService>().PlaySfxSound(SoundId.Success);
            ServiceLocator.Container.Single<IAudioService>().ChangeMusic(SoundId.WinLoop);
            DoubleRewardButtonActiveState.Value = ServiceLocator.Container.Single<IAdsService>().CheckIfRewardedIsAvailable();
            CollectedCoinsString.Value = GameplayModel.CurrentRunCoins.Value.ToString();
            ClearedLevelString.Value = ServiceLocator.Container.Single<ILocalizationService>().LocalizeTextByKey(LEVELCLEARED_KEY) + " " + ProgressModel.SessionProgress.CurrentLevel.ToString();

            // Multiply coins based on human count
            var coinMultiplier = 1 + (GameplayModel.CollectedHumans.Value - 1) * 0.1f;
            PrettyCoinIncrease(GameplayModel.CurrentRunCoins.Value, coinMultiplier);
        }

        protected override void SubscribeToModelChanges()
        {
            base.SubscribeToModelChanges();
            ProgressModel.SessionProgress.TotalCoins.Subscribe(value => TotalCoinsString.Value = value.ToString()).AddTo(CompositeDisposable);
        }

        public void QuitClicked()
        {
            ClosingActions();
            GameplayModel.LoadScene(SceneNames.MAIN_MENU, OnMenuLoaded);
        }

        public void ContinueClicked()
        {
            ClosingActions();
            GameplayModel.LoadScene(SceneNames.GAME, OnGameLoaded);
        }

        public void DoubleRewardButtonClicked()
        {
            var adService = ServiceLocator.Container.Single<IAdsService>();

            if (!adService.CheckIfRewardedIsAvailable()) return;

            DoubleRewardButtonActiveState.Value = false;
            _rewardedSubscription = adService.OnRewardedSuccess.Subscribe(_ => OnRewardGranted()).AddTo(CompositeDisposable);
            adService.ShowRewarded();
        }



        ///// INTERNAL

        private void ClosingActions()
        {
            ProgressModel.ModifyTotalCoins(GameplayModel.CurrentRunCoins.Value);
            ProgressModel.CompleteLevel();
            ProgressModel.SaveProgress();
        }

        private void PrettyCoinIncrease(int initialCoinValue, float coinMultiplier)
        {
            var multipliedCoins = Convert.ToInt32(initialCoinValue * coinMultiplier);
            AnimateCoinIncrease(initialCoinValue, multipliedCoins).Forget();
            GameplayModel.MultiplyRunCoinAmount(coinMultiplier);
        }

        private async UniTaskVoid AnimateCoinIncrease(int startingValue, int endingValue)
        {
            int value = startingValue;
            CollectedCoinsString.Value = value.ToString();

            while (value < endingValue)
            {
                await UniTask.Delay(_coinAnimationStep);
                value++;
                CollectedCoinsString.Value = value.ToString();
            }
        }

        private void OnGameLoaded()
        {
            GameplayModel.ChangeGameState(GameState.PreGame);
            GameplayModel.ActivateView(ViewId.PreGameView);
        }

        private void OnMenuLoaded()
        {
            GameplayModel.ChangeGameState(GameState.Menu);
            GameplayModel.ActivateView(ViewId.MainMenuView);
        }

        private void OnRewardGranted()
        {
            CompositeDisposable.Remove(_rewardedSubscription);
            PrettyCoinIncrease(GameplayModel.CurrentRunCoins.Value, 2);
        }

        public override void CloseView()
        {
            base.CloseView();
            ServiceLocator.Container.Single<IAudioService>().ChangeMusic(SoundId.MainTheme);
        }
    }
}
