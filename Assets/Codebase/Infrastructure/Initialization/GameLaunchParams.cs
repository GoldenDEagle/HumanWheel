using GamePush;
using System;
using UnityEngine;

namespace Assets.Codebase.Infrastructure.Initialization
{
    /// <summary>
    /// Launching parameters for testing
    /// </summary>
    [Serializable]
    public class GameLaunchParams
    {
        [Tooltip("If unchecked nothing else matters")]
        public bool ManualParamSet;
        [Space]
        [Tooltip("Startup language")]
        public Language Language;
        [Tooltip("Check to clear PlayerPrefs on startup")]
        public bool ClearPlayerPrefs;
        [Tooltip("Check to enable cheating")]
        public bool CheatsEnabled;

        public GameLaunchParams()
        {
            ManualParamSet = false;
            CheatsEnabled = false;
            ClearPlayerPrefs = false;
        }
    }
}
