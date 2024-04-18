using Assets.Codebase.Models.Gameplay.Data;
using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Utils.Values;
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
            GameplayModel.LoadScene(SceneNames.GAME, OnGameLoaded);
        }

        private void OnGameLoaded()
        {
            GameplayModel.ChangeGameState(GameState.PreGame);
            GameplayModel.ActivateView(ViewId.PreGameView);
        }
    }
}
