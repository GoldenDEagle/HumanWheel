using Assets.Codebase.Infrastructure.Initialization;
using Assets.Codebase.Models.Base;
using Assets.Codebase.Models.Gameplay.Data;
using Assets.Codebase.Views.Base;
using System;
using UniRx;

namespace Assets.Codebase.Models.Gameplay
{
    /// <summary>
    /// Model responsible for game flow.
    /// </summary>
    public interface IGameplayModel : IModel
    {
        /// <summary>
        /// Currently active view
        /// </summary>
        public ReactiveProperty<ViewId> ActiveViewId { get; }
        /// <summary>
        /// Called when target view is closed
        /// </summary>
        public Subject<ViewId> OnViewClosed { get; }
        /// <summary>
        /// Called when game state was changed
        /// </summary>
        public Subject<GameState> OnGameStateChanged { get; }
        /// <summary>
        /// Use to switch between views (deactivates all others)
        /// </summary>
        /// <param name="viewId"></param>
        public void ActivateView(ViewId viewId);
        /// <summary>
        /// Current game state
        /// </summary>
        public ReactiveProperty<GameState> State { get; }

        public void ChangeGameState(GameState state);

        public void LoadScene(string name, Action onLoaded = null);



        // RUN

        public ReactiveProperty<int> CurrentRunCoins { get; }
        public ReactiveProperty<int> CollectedHumans { get; }
        public Subject<Unit> OnHumanAdded { get; }

        /// <summary>
        /// Set humans collected during the run
        /// </summary>
        /// <param name="humanCount"></param>
        public void SetCollectedHumansCount(int humanCount);

        /// <summary>
        /// Adds delta coins
        /// </summary>
        /// <param name="deltaCoins"></param>
        public int ModifyRunCoinAmount(int deltaCoins);

        /// <summary>
        /// Multiplies current run coin amount
        /// </summary>
        /// <param name="multiplier"></param>
        public int MultiplyRunCoinAmount(float multiplier);
        public void AddHumanToWheel();
    }
}
