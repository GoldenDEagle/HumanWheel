using Assets.Codebase.Infrastructure.Initialization;
using Assets.Codebase.Models.Base;
using Assets.Codebase.Models.Gameplay.Data;
using Assets.Codebase.Views.Base;
using System;
using UniRx;

namespace Assets.Codebase.Models.Gameplay
{
    public class GameplayModel : BaseModel, IGameplayModel
    {
        // Internal
        private ReactiveProperty<GameState> _state;
        private ReactiveProperty<ViewId> _activeViewId;
        private Subject<ViewId> _onViewClosed;
        private SceneLoader _sceneLoader;


        // Public properties
        public ReactiveProperty<GameState> State => _state;
        public ReactiveProperty<ViewId> ActiveViewId => _activeViewId;
        public Subject<ViewId> OnViewClosed => _onViewClosed;

        // Current run info
        private ReactiveProperty<int> _currentRunCoins;
        public ReactiveProperty<int> CurrentRunCoins => _currentRunCoins;

        public GameplayModel()
        {
            _sceneLoader = new SceneLoader();

            _state = new ReactiveProperty<GameState>(GameState.None);
            _activeViewId = new ReactiveProperty<ViewId>(ViewId.None);
            _currentRunCoins = new ReactiveProperty<int>(0);

            _onViewClosed = new Subject<ViewId>();
        }

        public void InitModel()
        {
        }

        public void ActivateView(ViewId viewId)
        {
            if (ActiveViewId.Value == viewId) { return; }

            _onViewClosed.OnNext(ActiveViewId.Value);

            ActiveViewId.Value = viewId;
        }

        public void ChangeGameState(GameState state)
        {
            State.Value = state;
        }

        public void LoadScene(string name, Action onLoaded = null)
        {
            _sceneLoader.Load(name, onLoaded);
        }


        // RUN MANIPULATIONS

        public void ModifyRunCoinAmount(int deltaCoins)
        {
            _currentRunCoins.Value += deltaCoins;
        }

        public void MultiplyRunCoinAmount(int multiplier)
        {
            _currentRunCoins.Value *= multiplier;
        }
    }
}
