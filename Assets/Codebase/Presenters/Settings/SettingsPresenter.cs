using Assets.Codebase.Presenters.Base;
using Assets.Codebase.Views.Base;

namespace Assets.Codebase.Presenters.Settings
{
    public class SettingsPresenter : BasePresenter, ISettingsPresenter
    {
        public SettingsPresenter()
        {
            CorrespondingViewId = ViewId.SettingsView;
        }

        public void CloseClicked()
        {
            GameplayModel.ActivateView(ViewId.PreGameView);
            ProgressModel.SaveProgress();
        }

        public void EffectsSliderMoved(float newValue)
        {
            ProgressModel.SessionProgress.SFXVolume.Value = newValue;
        }

        public void MusicSliderMoved(float newValue)
        {
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
