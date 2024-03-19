using Assets.Codebase.Presenter.Base;
using Assets.Codebase.Presenters.Fail;
using Assets.Codebase.Views.Base;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Codebase.Views.Fail
{
    public class FailView : BaseView
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _quitButton;

        private IFailPresenter _presenter;

        public override void Init(IPresenter presenter)
        {
            _presenter = presenter as IFailPresenter;

            base.Init(_presenter);
        }

        protected override void SubscribeToUserInput()
        {
            _restartButton.OnClickAsObservable().Subscribe(_ => _presenter.RestartClicked()).AddTo(CompositeDisposable);
            _quitButton.OnClickAsObservable().Subscribe(_ => _presenter.QuitClicked()).AddTo(CompositeDisposable);
        }
    }
}