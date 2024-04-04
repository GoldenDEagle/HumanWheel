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
        public ReactiveProperty<string> CollectedCoinsString { get; private set; }

        private int _coinAnimationStep = 100;

        public EndgamePresenter()
        {
            CorrespondingViewId = ViewId.EndgameView;

            CollectedCoinsString = new ReactiveProperty<string>();
        }

        public override void CreateView()
        {
            base.CreateView();
            CollectedCoinsString.Value = GameplayModel.CurrentRunCoins.Value.ToString();

            // Multiply coins based on human count
            var coinMultiplier = 1 + (GameplayModel.CollectedHumans.Value - 1) * 0.1f;
            PrettyCoinIncrease(GameplayModel.CurrentRunCoins.Value, coinMultiplier);
        }

        protected override void SubscribeToModelChanges()
        {
            base.SubscribeToModelChanges();
        }

        public void QuitClicked()
        {
            GameplayModel.LoadScene(SceneNames.MAIN_MENU, () => { GameplayModel.ActivateView(ViewId.MainMenuView); });
        }

        public void ContinueClicked()
        {
            GameplayModel.LoadScene(SceneNames.GAME, () => { GameplayModel.ActivateView(ViewId.None); });
        }





        ///// INTERNAL

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
    }
}
