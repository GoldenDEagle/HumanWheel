using Assets.Codebase.Presenter.Base;
using UniRx;

namespace Assets.Codebase.Presenters.EndGame
{
    public interface IEndgamePresenter : IPresenter
    {
        public ReactiveProperty<string> ClearedLevelString { get; }
        public ReactiveProperty<string> TotalCoinsString { get; }
        public ReactiveProperty<string> CollectedCoinsString { get; }
        public ReactiveProperty<bool> DoubleRewardButtonActiveState { get; }

        public void ContinueClicked();
        public void QuitClicked();
        public void DoubleRewardButtonClicked();
    }
}
