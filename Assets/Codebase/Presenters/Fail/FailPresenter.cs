using Assets.Codebase.Data.Audio;
using Assets.Codebase.Infrastructure.ServicesManagment.Audio;
using Assets.Codebase.Infrastructure.ServicesManagment;
using Assets.Codebase.Models.Gameplay.Data;
using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Utils.Values;
using Assets.Codebase.Views.Base;
using System;
using Assets.Codebase.Infrastructure.ServicesManagment.Ads;
using UniRx;

namespace Assets.Codebase.Presenters.Fail
{
    public class FailPresenter : BasePresenter, IFailPresenter
    {
        private IDisposable _adSubscription;

        public FailPresenter()
        {
            CorrespondingViewId = ViewId.FailView;
        }

        public override void CreateView()
        {
            base.CreateView();
            ServiceLocator.Container.Single<IAudioService>().PlaySfxSound(SoundId.Failure);
        }

        public void QuitClicked()
        {
#if UNITY_EDITOR
            GameplayModel.LoadScene(SceneNames.MAIN_MENU, OnMenuLoaded);
            return;
#endif

            var adService = ServiceLocator.Container.Single<IAdsService>();

            if (!adService.CheckIfFullscreenIsAvailable())
            {
                QuitAfterAd();
                return;
            }

            _adSubscription = adService.OnFullscreenClosed.Subscribe(_ => QuitAfterAd()).AddTo(CompositeDisposable);
            adService.ShowFullscreen();
        }

        public void RestartClicked()
        {
#if UNITY_EDITOR
            GameplayModel.LoadScene(SceneNames.GAME, OnGameLoaded);
            return;
#endif

            var adService = ServiceLocator.Container.Single<IAdsService>();

            if (!adService.CheckIfFullscreenIsAvailable())
            {
                PlayAfterAd();
                return;
            }

            _adSubscription = adService.OnFullscreenClosed.Subscribe(_ => PlayAfterAd()).AddTo(CompositeDisposable);
            adService.ShowFullscreen();
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

        private void QuitAfterAd()
        {
            if ( CompositeDisposable.Contains(_adSubscription))
            {
                CompositeDisposable.Remove(_adSubscription);
            }
            GameplayModel.LoadScene(SceneNames.MAIN_MENU, OnMenuLoaded);
        }

        private void PlayAfterAd()
        {
            if (CompositeDisposable.Contains(_adSubscription))
            {
                CompositeDisposable.Remove(_adSubscription);
            }
            GameplayModel.LoadScene(SceneNames.GAME, OnGameLoaded);
        }
    }
}
