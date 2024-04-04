using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Views.Base;

namespace Assets.Codebase.Presenters.MainMenu
{
    public class MainMenuPresenter : BasePresenter, IMainMenuPresenter
    {
        public MainMenuPresenter()
        {
            CorrespondingViewId = ViewId.MainMenuView;
        }

        public void StartButtonClicked()
        {
            GameplayModel.LoadScene("Game", () => GameplayModel.ActivateView(ViewId.IngameView));
        }
    }
}
