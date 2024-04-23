using Assets.Codebase.Presenter.Base;
using UniRx;

namespace Assets.Codebase.Presenters.PreGame
{
    public interface IPreGamePresenter : IPresenter
    {
        public ReactiveProperty<string> TotalCoinsString { get; }
        public ReactiveProperty<string> CurrentLevelString { get; }
        public ReactiveProperty<bool> WatchAdButtonActiveState { get; }

        public void StartGameClicked();
        public void BuyHumanClicked();
        public void WatchAdForCoinsClicked();
        public void LeaderboardButtonClicked();
        public void SettingsClicked();
    }
}
