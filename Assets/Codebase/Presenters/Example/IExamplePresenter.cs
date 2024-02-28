using Assets.Codebase.Presenter.Base;
using UniRx;

namespace Assets.Codebase.Presenter.MainMenu
{
    public interface IExamplePresenter : IPresenter
    {
        public ReactiveProperty<string> StartButtonText { get; }
        public ReactiveProperty<string> SampleValueAmountString { get; }

        public void OnStartButtonClicked();
    }
}
