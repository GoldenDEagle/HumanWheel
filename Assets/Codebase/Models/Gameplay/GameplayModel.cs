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
        private Subject<GameState> _onGameStateChanged;
        private Subject<Unit> _onUnitAdded;
        private SceneLoader _sceneLoader;


        // Public properties
        public ReactiveProperty<GameState> State => _state;
        public ReactiveProperty<ViewId> ActiveViewId => _activeViewId;
        public Subject<ViewId> OnViewClosed => _onViewClosed;
        public Subject<GameState> OnGameStateChanged => _onGameStateChanged;
        public Subject<Unit> OnHumanAdded => _onUnitAdded;

        // Current run info
        private ReactiveProperty<int> _currentRunCoins;
        public ReactiveProperty<int> CurrentRunCoins => _currentRunCoins;
        private ReactiveProperty<int> _collectedHumans;
        public ReactiveProperty<int> CollectedHumans => _collectedHumans;

        

        public GameplayModel()
        {
            _sceneLoader = new SceneLoader();

            _state = new ReactiveProperty<GameState>(GameState.Bootstrap);
            _activeViewId = new ReactiveProperty<ViewId>(ViewId.None);
            _currentRunCoins = new ReactiveProperty<int>(0);
            _collectedHumans = new ReactiveProperty<int>(0);

            _onViewClosed = new Subject<ViewId>();
            _onGameStateChanged = new Subject<GameState>();
            _onUnitAdded = new Subject<Unit>();
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

            _onGameStateChanged.OnNext(state);
        }

        public void LoadScene(string name, Action onLoaded = null)
        {
            _sceneLoader.Load(name, onLoaded);
        }


        // RUN MANIPULATIONS

        public void SetCollectedHumansCount(int humanCount)
        {
            _collectedHumans.Value = humanCount;
        }

        public int ModifyRunCoinAmount(int deltaCoins)
        {
            _currentRunCoins.Value += deltaCoins;
            return _currentRunCoins.Value;
        }

        public int MultiplyRunCoinAmount(float multiplier)
        {
            var newCoinAmount = Convert.ToInt32(_currentRunCoins.Value * multiplier);
            _currentRunCoins.Value = newCoinAmount;
            return _currentRunCoins.Value;
        }

        public void AddHumanToWheel()
        {
            _onUnitAdded.OnNext(Unit.Default);
        }
    }
}
