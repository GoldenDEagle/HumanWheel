using Assets.Codebase.Infrastructure.ServicesManagment.ViewCreation;
using Assets.Codebase.Infrastructure.ServicesManagment;
using Assets.Codebase.Models.Gameplay;
using Assets.Codebase.Models.Progress;
using Assets.Codebase.Presenter.Base;
using Assets.Codebase.Views.Base;
using System;
using UniRx;

namespace Assets.Codebase.Presenters.Base
{
    public abstract class BasePresenter : IPresenter, IDisposable
    {
        // References to models
        protected IProgressModel ProgressModel;
        protected IGameplayModel GameplayModel;

        /// <summary>
        /// Container for all disposables.
        /// </summary>
        protected CompositeDisposable CompositeDisposable = new CompositeDisposable();

        /// <summary>
        /// Corresponding view Id.
        /// </summary>
        protected ViewId ViewId = ViewId.None;


        // Fire this to close view.
        public Subject<Unit> OnCloseView { get; private set; }

        private bool _isViewActive = false;

        /// <summary>
        /// Creates binding to models.
        /// </summary>
        /// <param name="progressModel"></param>
        /// <param name="gameplayModel"></param>
        public void SetupModels(IProgressModel progressModel, IGameplayModel gameplayModel)
        {
            ProgressModel = progressModel;
            GameplayModel = gameplayModel;

            OnCloseView = new Subject<Unit>();

            SubscribeToModelChanges();
        }

        /// <summary>
        /// Subscribe to all interesting model parameters.
        /// </summary>
        protected virtual void SubscribeToModelChanges()
        {
            GameplayModel.ActiveViewId.Where(activeView => activeView == ViewId).Subscribe(_ => CreateView()).AddTo(CompositeDisposable);
            GameplayModel.OnViewClosed.Where(activeView => activeView == ViewId).Subscribe(_ => CloseView()).AddTo(CompositeDisposable);
        }

        public void CloseView()
        {
            if (!_isViewActive) return;

            _isViewActive = false;
            OnCloseView.OnNext(Unit.Default);
        }

        /// <summary>
        /// Gets ID of corresponding view.
        /// </summary>
        /// <returns></returns>
        public ViewId GetCorrespondingViewId()
        {
            return ViewId;
        }

        /// <summary>
        /// Creates corresponding view
        /// </summary>
        public virtual void CreateView()
        {
            _isViewActive = true;
            var view = ServiceLocator.Container.Single<IViewCreatorService>().CreateView(ViewId);
            view.Init(this);
        }

        public void Dispose()
        {
            CompositeDisposable.Dispose();
        }
    }
}
