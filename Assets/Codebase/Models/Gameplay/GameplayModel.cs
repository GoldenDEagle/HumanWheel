using Assets.Codebase.Models.Base;
using Assets.Codebase.Models.Gameplay.Data;
using Assets.Codebase.Views.Base;
using UniRx;

namespace Assets.Codebase.Models.Gameplay
{
    public class GameplayModel : BaseModel, IGameplayModel
    {
        // Internal
        private ReactiveProperty<GameState> _state;
        private ReactiveProperty<ViewId> _activeViewId;
        private Subject<ViewId> _onViewClosed;


        // Public properties
        public ReactiveProperty<GameState> State => _state;
        public ReactiveProperty<ViewId> ActiveViewId => _activeViewId;
        public Subject<ViewId> OnViewClosed => _onViewClosed;

        public GameplayModel()
        {
            _state = new ReactiveProperty<GameState>(GameState.None);
            _activeViewId = new ReactiveProperty<ViewId>(ViewId.None);
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
    }
}
