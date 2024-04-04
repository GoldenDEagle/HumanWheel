using Assets.Codebase.Presenter.Base;
using Assets.Codebase.Presenters.Ingame;
using Assets.Codebase.Utils.Extensions;
using Assets.Codebase.Views.Base;
using TMPro;
using UniRx;
using UnityEngine;

namespace Assets.Codebase.Views.Ingame
{
    public class IngameView : BaseView
    {
        [SerializeField] private TMP_Text _coinsText;

        private IIngamePresenter _presenter;

        public override void Init(IPresenter presenter)
        {
            _presenter = presenter as IIngamePresenter;

            base.Init(_presenter);
        }

        protected override void SubscribeToPresenterEvents()
        {
            base.SubscribeToPresenterEvents();
            _presenter.CollectedCoinsString.SubscribeToTMPText(_coinsText).AddTo(CompositeDisposable);
        }

        protected override void SubscribeToUserInput()
        {
        }
    }
}