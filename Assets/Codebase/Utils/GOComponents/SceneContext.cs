﻿using Assets.Codebase.Infrastructure.Initialization;
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
            var musicSource = new GameObject().AddComponent<AudioSource>();
            var effectsSource = new GameObject().AddComponent<AudioSource>();

            GameStructure structure = new GameStructure(uiRoot, musicSource, effectsSource, _gameLaunchParams);
        }
#endif
    }
}