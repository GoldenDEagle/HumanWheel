using UniRx;

namespace Assets.Codebase.Models.Progress.Data
{
    /// <summary>
    /// Used at runtime.
    /// </summary>
    public class SessionProgress
    {
        // All the properties that need to be saved...

        public ReactiveProperty<int> CurrentLevel;
        public ReactiveProperty<int> TotalCoins;
        public ReactiveProperty<float> MusicVolume;
        public ReactiveProperty<float> SFXVolume;

        // .

        /// <summary>
        /// Creates new progress with default values.
        /// </summary>
        public SessionProgress()
        {
            CurrentLevel = new ReactiveProperty<int>(1);
            TotalCoins = new ReactiveProperty<int>(0);
            MusicVolume = new ReactiveProperty<float>(0.25f);
            SFXVolume = new ReactiveProperty<float>(0.75f);
        }

        /// <summary>
        /// Creates new progress from persistant data.
        /// </summary>
        /// <param name="progress"></param> Progress to initialize from
        public SessionProgress(PersistantProgress progress)
        {
            CurrentLevel = new ReactiveProperty<int>(progress.CurrentLevel);
            TotalCoins = new ReactiveProperty<int>(progress.TotalCoins);
            MusicVolume = new ReactiveProperty<float>(progress.MusicVolume);
            SFXVolume = new ReactiveProperty<float>(progress.SFXVolume);
        }
    }
}
