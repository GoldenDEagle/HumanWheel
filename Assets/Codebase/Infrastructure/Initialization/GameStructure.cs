using Assets.Codebase.Infrastructure.ServicesManagment;
using Assets.Codebase.Infrastructure.ServicesManagment.Ads;
using Assets.Codebase.Infrastructure.ServicesManagment.Assets;
using Assets.Codebase.Infrastructure.ServicesManagment.Audio;
using Assets.Codebase.Infrastructure.ServicesManagment.Gameplay;
using Assets.Codebase.Infrastructure.ServicesManagment.Localization;
using Assets.Codebase.Infrastructure.ServicesManagment.PresenterManagement;
using Assets.Codebase.Infrastructure.ServicesManagment.Progress;
using Assets.Codebase.Infrastructure.ServicesManagment.ViewCreation;
using Assets.Codebase.Models.Gameplay;
using Assets.Codebase.Models.Progress;
using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Presenters.Example;
using GamePush;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Codebase.Infrastructure.Initialization
{
    /// <summary>
    /// Responsible for creation of game structure.
    /// </summary>
    public class GameStructure
    {
        public static bool IsGameInitialized = false;
        public static GameLaunchParams GameLaunchParameters { get; private set; }

        // Needed from outside
        private RectTransform _uiRoot;

        // Created inside
        private IProgressModel _progressModel;
        private IGameplayModel _gameplayModel;
        private List<BasePresenter> _presenters;

        public GameStructure(RectTransform uiRoot, GameLaunchParams launchParams = null)
        {
            if (IsGameInitialized) { return; }
            IsGameInitialized = true;

            _uiRoot = uiRoot;

            GameLaunchParameters = launchParams ?? new GameLaunchParams();

            ApplyPreloadParams();

            CreateMVPStructure();
            RegisterServices();
            // Fills data using asset provider
            _gameplayModel.InitModel();

            ApplyAfterLoadParams();
        }

        // MVP structure
        private void CreateMVPStructure()
        {
            CreateModels();
            CreatePresenters();
        }

        private void CreateModels()
        {
            _progressModel = new LocalProgressModel();
            _gameplayModel = new GameplayModel();
        }

        private void CreatePresenters()
        {
            // Create presenter for each view
            _presenters = new List<BasePresenter>();
            _presenters.Add(new ExamplePresenter());

            foreach (var presenter in _presenters)
            {
                presenter.SetupModels(_progressModel, _gameplayModel);
            }
        }


        /// <summary>
        /// Registering all game services.
        /// </summary>
        private void RegisterServices()
        {
            var services = ServiceLocator.Container;

            services.RegisterSingle<IAssetProvider>(new AssetProvider());
            services.RegisterSingle<IViewCreatorService>(new ViewCreatorService(services.Single<IAssetProvider>(), _presenters, _uiRoot));
            services.RegisterSingle<IAdsService>(new GamePushAdService());
            services.RegisterSingle<IAudioService>(new AudioService(services.Single<IAssetProvider>(), _progressModel));
            services.RegisterSingle<IGameplayService>(new GameplayService(_gameplayModel));
            services.RegisterSingle<ILocalizationService>(new GoogleSheetLocalizationService());
            services.RegisterSingle<IPresentersService>(new PresentersService(_presenters));
            services.RegisterSingle<IProgressService>(new ProgressService(_progressModel));
        }


        // Launch params handling.
        /// <summary>
        /// Before model and service initialization.
        /// </summary>
        private void ApplyPreloadParams()
        {
            if (GameLaunchParameters.ManualParamSet)
            {
                if (GameLaunchParameters.ClearPlayerPrefs)
                {
                    PlayerPrefs.DeleteAll();
                }
            }
        }

        /// <summary>
        /// After model and service initialization.
        /// </summary>
        private void ApplyAfterLoadParams()
        {
            var services = ServiceLocator.Container;

            if (GameLaunchParameters.ManualParamSet)
            {
                services.Single<ILocalizationService>().SetLanguage(GameLaunchParameters.Language);
            }
            else
            {
                services.Single<ILocalizationService>().SetLanguage(GP_Language.Current());
            }
        }
    }
}
