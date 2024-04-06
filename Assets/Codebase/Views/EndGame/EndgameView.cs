using Assets.Codebase.Presenter.Base;
using Assets.Codebase.Presenters.EndGame;
using Assets.Codebase.Utils.Extensions;
using Assets.Codebase.Views.Base;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Codebase.Views.EndGame
{
    public class EndgameView : BaseView
    {
        [SerializeField] private TMP_Text _totalCoinsText;
        [SerializeField] private TMP_Text _collectedCoinsText;
        [SerializeField] private TMP_Text _clearedLevelText;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _quitButton;

        private IEndgamePresenter _presenter;

        public override void Init(IPresenter presenter)
        {
            _presenter = presenter as IEndgamePresenter;

            base.Init(_presenter);
        }

        protected override void SubscribeToPresenterEvents()
        {
            base.SubscribeToPresenterEvents();
            _presenter.CollectedCoinsString.SubscribeToTMPText(_collectedCoinsText).AddTo(CompositeDisposable);
            _presenter.TotalCoinsString.SubscribeToTMPText(_totalCoinsText).AddTo(CompositeDisposable);
            _presenter.ClearedLevelString.SubscribeToTMPText(_clearedLevelText).AddTo(CompositeDisposable);
        }

        protected override void SubscribeToUserInput()
        {
            _continueButton.OnClickAsObservable().Subscribe(_ => _presenter.ContinueClicked()).AddTo(CompositeDisposable);
            _quitButton.OnClickAsObservable().Subscribe(_ => _presenter.QuitClicked()).AddTo(CompositeDisposable);
        }
    }
}