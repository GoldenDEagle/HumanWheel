using Assets.Codebase.Models.Gameplay.Data;
using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Views.Base;
using UniRx;

namespace Assets.Codebase.Presenters.PreGame
{
    public class PreGamePresenter : BasePresenter, IPreGamePresenter
    {
        public ReactiveProperty<string> TotalCoinsString { get; private set; }
        public ReactiveProperty<string> CurrentLevelString { get; private set; }

        public PreGamePresenter()
        {
            CorrespondingViewId = ViewId.PreGameView;

            CurrentLevelString = new ReactiveProperty<string>();
            TotalCoinsString = new ReactiveProperty<string>();
        }

        protected override void SubscribeToModelChanges()
        {
            base.SubscribeToModelChanges();
            ProgressModel.SessionProgress.TotalCoins.Subscribe(value => TotalCoinsString.Value = value.ToString()).AddTo(CompositeDisposable);
            ProgressModel.SessionProgress.CurrentLevel.Subscribe(value => CurrentLevelString.Value = value.ToString()).AddTo(CompositeDisposable);
        }

        public void StartGameClicked()
        {
            GameplayModel.ChangeGameState(GameState.Game);
            GameplayModel.ActivateView(ViewId.IngameView);
        }

        public void BuyHumanClicked()
        {
            GameplayModel.AddHumanToWheel();
        }

        public void WatchAdForCoinsClicked()
        {

        }
    }
}
