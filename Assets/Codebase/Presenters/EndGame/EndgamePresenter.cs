using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Utils.Values;
using Assets.Codebase.Views.Base;

namespace Assets.Codebase.Presenters.EndGame
{
    public class EndgamePresenter : BasePresenter, IEndgamePresenter
    {
        public EndgamePresenter()
        {
            CorrespondingViewId = ViewId.EndgameView;
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
