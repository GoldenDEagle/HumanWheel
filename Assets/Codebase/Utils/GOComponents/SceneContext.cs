using Assets.Codebase.Infrastructure.Initialization;
using UnityEngine;

namespace Assets.Codebase.Utils.GOComponents
{
    /// <summary>
    /// Used to initialize game structure outside Bootstrapper scene.
    /// </summary>
    public class SceneContext : MonoBehaviour
    {
        [SerializeField] private RectTransform _uiRootPrefab;
        [SerializeField] GameLaunchParams _gameLaunchParams;

#if UNITY_EDITOR

        private void Awake()
        {
            if (GameStructure.IsGameInitialized) return;

            var uiRoot = Instantiate(_uiRootPrefab);

            GameStructure structure = new GameStructure(uiRoot, _gameLaunchParams);
        }
#endif
    }
}