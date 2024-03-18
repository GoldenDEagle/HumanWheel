using Assets.Codebase.Presenter.Base;
using Assets.Codebase.Presenters.MainMenu;
using Assets.Codebase.Views.Base;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Codebase.Views.MainMenu
{
    public class MainMenuView : BaseView
    {
        [SerializeField] private Button _startButton;

        private IMainMenuPresenter _presenter;

        public override void Init(IPresenter presenter)
        {
            _presenter = presenter as IMainMenuPresenter;

            base.Init(_presenter);
        }

        protected override void SubscribeToUserInput()
        {
            _startButton.OnClickAsObservable().Subscribe(_ => _presenter.StartButtonClicked()).AddTo(CompositeDisposable);
        }
    }
}