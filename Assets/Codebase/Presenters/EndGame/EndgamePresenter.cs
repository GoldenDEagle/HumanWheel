using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Utils.Values;
using Assets.Codebase.Views.Base;
using UniRx;

namespace Assets.Codebase.Presenters.EndGame
{
    public class EndgamePresenter : BasePresenter, IEndgamePresenter
    {
        public ReactiveProperty<string> CollectedCoinsString { get; private set; }

        public EndgamePresenter()
        {
            CorrespondingViewId = ViewId.EndgameView;

            CollectedCoinsString = new ReactiveProperty<string>();
        }

        protected override void SubscribeToModelChanges()
        {
            base.SubscribeToModelChanges();
            GameplayModel.CurrentRunCoins.Subscribe(value => CollectedCoinsString.Value = value.ToString()).AddTo(CompositeDisposable);
        }

        public void QuitClicked()
        {
            GameplayModel.LoadScene(SceneNames.MAIN_MENU, () => { GameplayModel.ActivateView(ViewId.MainMenuView); });
        }

        public void ContinueClicked()
        {
            GameplayModel.LoadScene(SceneNames.GAME, () => { GameplayModel.ActivateView(ViewId.None); });
        }
    }
}
