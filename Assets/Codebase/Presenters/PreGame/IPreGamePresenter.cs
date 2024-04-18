using Assets.Codebase.Presenter.Base;
using UniRx;

namespace Assets.Codebase.Presenters.PreGame
{
    public interface IPreGamePresenter : IPresenter
    {
        public ReactiveProperty<string> TotalCoinsString { get; }
        public ReactiveProperty<string> CurrentLevelString { get; }

        public void StartGameClicked();
        public void BuyHumanClicked();
        public void WatchAdForCoinsClicked();
    }
}
