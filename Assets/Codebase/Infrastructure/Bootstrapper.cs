using Assets.Codebase.Infrastructure.Initialization;
using Assets.Codebase.Infrastructure.ServicesManagment;
using Assets.Codebase.Infrastructure.ServicesManagment.Gameplay;
using Assets.Codebase.Utils.GOComponents;
using Assets.Codebase.Views.Base;
using UnityEngine;

namespace Assets.Codebase.Infrastructure
{
    [RequireComponent(typeof(DontDestroyOnLoadComponent))]
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private RectTransform _uiRoot;
        [SerializeField] private GameLaunchParams _launchParams;

        private void Awake()
        {
            // Create game structure
            GameStructure structure = new GameStructure(_uiRoot, _launchParams);

            // Start the game
            ServiceLocator.Container.Single<IGameplayService>().GameplayModel.ActivateView(ViewId.ExampleView);
        }
    }
}

