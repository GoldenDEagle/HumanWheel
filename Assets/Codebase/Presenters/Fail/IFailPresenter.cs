using Assets.Codebase.Presenter.Base;

namespace Assets.Codebase.Presenters.Fail
{
    public interface IFailPresenter : IPresenter
    {
        public void RestartClicked();
        public void QuitClicked();
    }
}
