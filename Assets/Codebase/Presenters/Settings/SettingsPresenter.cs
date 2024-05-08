using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Views.Base;
using UniRx;

namespace Assets.Codebase.Presenters.Settings
{
    public class SettingsPresenter : BasePresenter, ISettingsPresenter
    {
        public Subject<Unit> OnInitialSet { get; private set; }

        private bool _viewInitialized = false;

        public SettingsPresenter()
        {
            CorrespondingViewId = ViewId.SettingsView;
            OnInitialSet = new Subject<Unit>();
        }

        public override void CreateView()
        {
            base.CreateView();
            OnInitialSet.OnNext(Unit.Default);
            _viewInitialized = true;
        }

        public void CloseClicked()
        {
            GameplayModel.ActivateView(ViewId.PreGameView);
            ProgressModel.SaveProgress();
            _viewInitialized = false;
        }

        public void EffectsSliderMoved(float newValue)
        {
            if (!_viewInitialized) return;

            ProgressModel.SessionProgress.SFXVolume.Value = newValue;
        }

        public void MusicSliderMoved(float newValue)
        {
            if (!_viewInitialized) return;

            ProgressModel.SessionProgress.MusicVolume.Value = newValue;
        }



        public float GetCurrentMusicVolume()
        {
            return ProgressModel.SessionProgress.MusicVolume.Value;
        }
        public float GetCurrentEffectsVolume()
        {
            return ProgressModel.SessionProgress.SFXVolume.Value;
        }
    }
}
