using Assets.Codebase.Presenter.Base;
using UniRx;

namespace Assets.Codebase.Presenters.Ingame
{
    public interface IIngamePresenter : IPresenter
    {
        public ReactiveProperty<string> CollectedCoinsString { get; }
    }
}
