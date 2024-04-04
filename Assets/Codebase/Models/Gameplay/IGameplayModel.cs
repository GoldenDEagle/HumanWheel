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



        // RUN INFO

        public ReactiveProperty<int> CurrentRunCoins { get; }

        /// <summary>
        /// Adds delta coins
        /// </summary>
        /// <param name="deltaCoins"></param>
        public void ModifyRunCoinAmount(int deltaCoins);

        /// <summary>
        /// Multiplies current run coin amount
        /// </summary>
        /// <param name="multiplier"></param>
        public void MultiplyRunCoinAmount(int multiplier);
    }
}
