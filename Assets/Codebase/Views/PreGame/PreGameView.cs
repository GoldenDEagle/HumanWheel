using Assets.Codebase.Presenter.Base;
using Assets.Codebase.Presenters.PreGame;
using Assets.Codebase.Utils.Extensions;
using Assets.Codebase.Views.Base;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Codebase.Views.PreGame
{
    public class PreGameView : BaseView
    {
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _buyHumanButton;
        [SerializeField] private Button _freeCoinsButton;
        [SerializeField] private TMP_Text _totalCoinsText;
        [SerializeField] private TMP_Text _currentLevelText;

        private IPreGamePresenter _presenter;

        public override void Init(IPresenter presenter)
        {
            _presenter = presenter as IPreGamePresenter;

            base.Init(_presenter);
        }

        protected override void SubscribeToUserInput()
        {
            _buyHumanButton.OnClickAsObservable().Subscribe(_ => _presenter.BuyHumanClicked()).AddTo(CompositeDisposable);
            _freeCoinsButton.OnClickAsObservable().Subscribe(_ => _presenter.WatchAdForCoinsClicked()).AddTo(CompositeDisposable);
        }

        protected override void SubscribeToPresenterEvents()
        {
            base.SubscribeToPresenterEvents();
            _presenter.CurrentLevelString.SubscribeToTMPText(_currentLevelText).AddTo(CompositeDisposable);
            _presenter.TotalCoinsString.SubscribeToTMPText(_totalCoinsText).AddTo(CompositeDisposable);
        }

        public void StartGameClicked()
        {
            _presenter.StartGameClicked();
        }
    }
}