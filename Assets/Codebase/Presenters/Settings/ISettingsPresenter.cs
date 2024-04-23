using Assets.Codebase.Models.Progress;
using Assets.Codebase.Presenter.Base;

namespace Assets.Codebase.Presenters.Settings
{
    public interface ISettingsPresenter : IPresenter
    {
        public void CloseClicked();
        public void MusicSliderMoved(float newValue);
        public void EffectsSliderMoved(float newValue);

        public float GetCurrentMusicVolume();
        public float GetCurrentEffectsVolume();
    }
}
