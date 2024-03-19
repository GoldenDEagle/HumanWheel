using Assets.Codebase.Presenter.Base;

namespace Assets.Codebase.Presenters.EndGame
{
    public interface IEndgamePresenter : IPresenter
    {
        public void ContinueClicked();
        public void QuitClicked();
    }
}
