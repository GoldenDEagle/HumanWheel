using Assets.Codebase.Data.Audio;
using Assets.Codebase.Infrastructure.ServicesManagment;
using Assets.Codebase.Infrastructure.ServicesManagment.Audio;
using Assets.Codebase.Presenter.Base;
using DG.Tweening;
using UniRx;
using UnityEngine;

namespace Assets.Codebase.Views.Base
{
    public abstract class BaseView : MonoBehaviour
    {
        [SerializeField] private bool _closeWindowAnimation = false;
        [SerializeField] private bool _closeWindowSound = false;
        [SerializeField] private RectTransform _window;

        protected IPresenter Presenter;

        /// <summary>
        /// Creates binding with corresponding presenter.
        /// </summary>
        /// <param name="presenter">Reference to presenter (unique for each view).</param>
        public virtual void Init(IPresenter presenter)
        {
            Presenter = presenter;
            SubscribeToUserInput();
            SubscribeToPresenterEvents();
        }

        /// <summary>
        /// All disposables container. Gets cleared on disable.
        /// </summary>
        protected CompositeDisposable CompositeDisposable = new CompositeDisposable();

        protected virtual void OnEnable()
        {
            if (_window == null) return;

            _window.localScale = Vector3.zero;
            _window.DOScale(Vector3.one, 1f).SetEase(Ease.InOutBack);
        }

        protected virtual void OnDisable() 
        {
            CompositeDisposable.Dispose();
        }

        /// <summary>
        /// Handle all user interactions (buttons, toogles, input fields, etc.)
        /// </summary>
        protected abstract void SubscribeToUserInput();

        /// <summary>
        /// Handle presenter commands that change view display.
        /// </summary>
        protected virtual void SubscribeToPresenterEvents()
        {
            // List all common presenter functions here:
            Presenter.OnCloseView.Subscribe(_ => CloseView()).AddTo(CompositeDisposable);
        }

        /// <summary>
        /// Common method to close view.
        /// </summary>
        private void CloseView()
        {
            if (_closeWindowSound)
            {
                ServiceLocator.Container.Single<IAudioService>().PlaySfxSound(SoundId.UIWindowClosed);
            }

            if (_window == null || _closeWindowAnimation == false)
            {
                DestroyView();
                return;
            }

            _window.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(DestroyView);
        }

        private void DestroyView()
        {
            Destroy(gameObject);
        }
    }
}