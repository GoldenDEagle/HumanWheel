using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Views.Base;
using UniRx;

namespace Assets.Codebase.Presenters.Ingame
{
    public class IngamePresenter : BasePresenter, IIngamePresenter
    {
        public ReactiveProperty<string> CollectedCoinsString { get; private set; }

        public IngamePresenter()
        {
            CorrespondingViewId = ViewId.IngameView;

            CollectedCoinsString = new ReactiveProperty<string>();
        }

        protected override void SubscribeToModelChanges()
        {
            base.SubscribeToModelChanges();
            GameplayModel.CurrentRunCoins.Subscribe(value => CollectedCoinsString.Value = value.ToString()).AddTo(CompositeDisposable);
        }
    }
}
