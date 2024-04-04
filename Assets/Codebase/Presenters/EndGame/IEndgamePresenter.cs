using Assets.Codebase.Presenter.Base;
using UniRx;

namespace Assets.Codebase.Presenters.EndGame
{
    public interface IEndgamePresenter : IPresenter
    {
        public ReactiveProperty<string> CollectedCoinsString { get; }

        public void ContinueClicked();
        public void QuitClicked();
    }
}
