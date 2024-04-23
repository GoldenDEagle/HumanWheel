using Assets.Codebase.Infrastructure.ServicesManagment;
using Assets.Codebase.Infrastructure.ServicesManagment.Ads;
using Assets.Codebase.Infrastructure.ServicesManagment.Assets;
using Assets.Codebase.Infrastructure.ServicesManagment.Audio;
using Assets.Codebase.Infrastructure.ServicesManagment.Factories;
using Assets.Codebase.Infrastructure.ServicesManagment.Localization;
using Assets.Codebase.Infrastructure.ServicesManagment.ModelAccess;
using Assets.Codebase.Infrastructure.ServicesManagment.PresenterManagement;
using Assets.Codebase.Infrastructure.ServicesManagment.ViewCreation;
using Assets.Codebase.Models.Gameplay;
using Assets.Codebase.Models.Progress;
using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Presenters.EndGame;
using Assets.Codebase.Presenters.Example;
using Assets.Codebase.Presenters.Fail;
using Assets.Codebase.Presenters.Ingame;
using Assets.Codebase.Presenters.MainMenu;
using Assets.Codebase.Presenters.PreGame;
using Assets.Codebase.Presenters.Settings;
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
        private AudioSource _effectsSource;
        private AudioSource _musicSource;

        public GameStructure(RectTransform uiRoot, AudioSource musicSource, AudioSource effectsSource, GameLaunchParams launchParams = null)
        {
            if (IsGameInitialized) { return; }
            IsGameInitialized = true;

            _uiRoot = uiRoot;
            _musicSource = musicSource;
            _effectsSource = effectsSource;

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
#if UNITY_EDITOR
            _progressModel = new LocalProgressModel();
#else
            _progressModel = new ServerProgressModel();
#endif

            _gameplayModel = new GameplayModel();
        }

        private void CreatePresenters()
        {
            // Create presenter for each view
            _presenters = new List<BasePresenter>
            {
                new ExamplePresenter(),
                new MainMenuPresenter(),
                new EndgamePresenter(),
                new FailPresenter(),
                new IngamePresenter(),
                new PreGamePresenter(),
                new SettingsPresenter(),
            };

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
            services.RegisterSingle<IAudioService>(new AudioService(services.Single<IAssetProvider>(), _progressModel, _effectsSource, _musicSource));
            services.RegisterSingle<IAdsService>(new GamePushAdService(services.Single<IAudioService>()));
            services.RegisterSingle<ILocalizationService>(new GoogleSheetLocalizationService());
            services.RegisterSingle<IPresentersService>(new PresentersService(_presenters));
            services.RegisterSingle<IModelAccesService>(new ModelAccessService(_progressModel, _gameplayModel));
            services.RegisterSingle<IGOFactory>(new GOFactory(services.Single<IAssetProvider>()));
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
