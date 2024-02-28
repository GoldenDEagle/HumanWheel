using System;
using UniRx;

namespace Assets.Codebase.Presenter.Base
{
    /// <summary>
    /// Common presenter functions.
    /// </summary>
    public interface IPresenter
    {
        public Subject<Unit> OnCloseView { get; }

        public void CloseView();
    }
}
