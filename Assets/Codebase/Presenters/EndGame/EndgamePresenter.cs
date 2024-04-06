using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Utils.Values;
using Assets.Codebase.Views.Base;
using Cysharp.Threading.Tasks;
using System;
using UniRx;

namespace Assets.Codebase.Presenters.EndGame
{
    public class EndgamePresenter : BasePresenter, IEndgamePresenter
    {
        public ReactiveProperty<string> ClearedLevelString { get; private set; }
        public ReactiveProperty<string> TotalCoinsString { get; private set; }
        public ReactiveProperty<string> CollectedCoinsString { get; private set; }

        private int _coinAnimationStep = 100;

        public EndgamePresenter()
        {
            CorrespondingViewId = ViewId.EndgameView;

            ClearedLevelString = new ReactiveProperty<string>();
            TotalCoinsString = new ReactiveProperty<string>();
            CollectedCoinsString = new ReactiveProperty<string>();
        }

        public override void CreateView()
        {
            base.CreateView();
            CollectedCoinsString.Value = GameplayModel.CurrentRunCoins.Value.ToString();
            ClearedLevelString.Value = "CLEARED LEVEL " + ProgressModel.SessionProgress.CurrentLevel.ToString();

            // Multiply coins based on human count
            var coinMultiplier = 1 + (GameplayModel.CollectedHumans.Value - 1) * 0.1f;
            PrettyCoinIncrease(GameplayModel.CurrentRunCoins.Value, coinMultiplier);
        }

        protected override void SubscribeToModelChanges()
        {
            base.SubscribeToModelChanges();
            ProgressModel.SessionProgress.TotalCoins.Subscribe(value => TotalCoinsString.Value = "TOTAL COINS: " + value).AddTo(CompositeDisposable);
        }

        public void QuitClicked()
        {
            ClosingActions();
            GameplayModel.LoadScene(SceneNames.MAIN_MENU, () => { GameplayModel.ActivateView(ViewId.MainMenuView); });
        }

        public void ContinueClicked()
        {
            ClosingActions();
            GameplayModel.LoadScene(SceneNames.GAME, () => { GameplayModel.ActivateView(ViewId.IngameView); });
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
            CollectedCoinsString.Value = "COLLECTED COINS: " + value.ToString();

            while (value < endingValue)
            {
                await UniTask.Delay(_coinAnimationStep);
                value++;
                CollectedCoinsString.Value = "COLLECTED COINS: " + value.ToString();
            }
        }
    }
}
