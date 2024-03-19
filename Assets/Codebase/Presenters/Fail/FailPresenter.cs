using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Utils.Values;
using Assets.Codebase.Views.Base;

namespace Assets.Codebase.Presenters.Fail
{
    public class FailPresenter : BasePresenter, IFailPresenter
    {
        public FailPresenter()
        {
            CorrespondingViewId = ViewId.FailView;
        }

        public void QuitClicked()
        {
            GameplayModel.LoadScene(SceneNames.MAIN_MENU, () => { GameplayModel.ActivateView(ViewId.MainMenuView); });
        }

        public void RestartClicked()
        {
            GameplayModel.LoadScene(SceneNames.GAME, () => { GameplayModel.ActivateView(ViewId.None); });
        }
    }
}
