using Assets.Codebase.Presenter.Base;
using Assets.Codebase.Presenters.EndGame;
using Assets.Codebase.Views.Base;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Codebase.Views.EndGame
{
    public class EndgameView : BaseView
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _quitButton;

        private IEndgamePresenter _presenter;

        public override void Init(IPresenter presenter)
        {
            _presenter = presenter as IEndgamePresenter;

            base.Init(_presenter);
        }

        protected override void SubscribeToUserInput()
        {
            _continueButton.OnClickAsObservable().Subscribe(_ => _presenter.ContinueClicked()).AddTo(CompositeDisposable);
            _quitButton.OnClickAsObservable().Subscribe(_ => _presenter.QuitClicked()).AddTo(CompositeDisposable);
        }
    }
}