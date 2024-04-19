using Assets.Codebase.Data.Audio;
using Assets.Codebase.Infrastructure.ServicesManagment.Audio;
using Assets.Codebase.Infrastructure.ServicesManagment;
using Assets.Codebase.Models.Gameplay.Data;
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

        public override void CreateView()
        {
            base.CreateView();
            ServiceLocator.Container.Single<IAudioService>().PlaySfxSound(SoundId.GameLost);
        }

        public void QuitClicked()
        {
            GameplayModel.LoadScene(SceneNames.MAIN_MENU, OnMenuLoaded);
        }

        public void RestartClicked()
        {
            GameplayModel.LoadScene(SceneNames.GAME, OnGameLoaded);
        }




        private void OnGameLoaded()
        {
            GameplayModel.ChangeGameState(GameState.PreGame);
            GameplayModel.ActivateView(ViewId.PreGameView);
        }

        private void OnMenuLoaded()
        {
            GameplayModel.ChangeGameState(GameState.Menu);
            GameplayModel.ActivateView(ViewId.MainMenuView);
        }
    }
}
