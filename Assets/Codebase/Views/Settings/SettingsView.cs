using Assets.Codebase.Presenter.Base;
using Assets.Codebase.Presenters.Settings;
using Assets.Codebase.Views.Base;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Codebase.Views.Settings
{
    public class SettingsView : BaseView
    {
        [SerializeField] private Button _closeWindowButton;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _effectsSlider;

        private ISettingsPresenter _presenter;

        public override void Init(IPresenter presenter)
        {
            _presenter = presenter as ISettingsPresenter;

            base.Init(_presenter);

            _musicSlider.SetValueWithoutNotify(_presenter.GetCurrentMusicVolume());
            _effectsSlider.SetValueWithoutNotify(_presenter.GetCurrentEffectsVolume());
        }

        protected override void SubscribeToUserInput()
        {
            _musicSlider.OnValueChangedAsObservable().Subscribe(value => _presenter.MusicSliderMoved(value)).AddTo(CompositeDisposable);
            _effectsSlider.OnValueChangedAsObservable().Subscribe(value => _presenter.EffectsSliderMoved(value)).AddTo(CompositeDisposable);
            _closeWindowButton.OnClickAsObservable().Subscribe(_ => _presenter.CloseClicked()).AddTo(CompositeDisposable);
        }
    }
}