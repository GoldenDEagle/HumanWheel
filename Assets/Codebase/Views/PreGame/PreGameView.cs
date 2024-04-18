using Assets.Codebase.Presenter.Base;
using Assets.Codebase.Presenters.PreGame;
using Assets.Codebase.Views.Base;
using UnityEngine;

namespace Assets.Codebase.Views.PreGame
{
    public class PreGameView : BaseView
    {
        private IPreGamePresenter _presenter;

        public override void Init(IPresenter presenter)
        {
            _presenter = presenter as IPreGamePresenter;

            base.Init(_presenter);
        }

        protected override void SubscribeToUserInput()
        {
        }
    }
}