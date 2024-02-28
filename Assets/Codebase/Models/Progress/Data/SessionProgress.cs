using UniRx;

namespace Assets.Codebase.Models.Progress.Data
{
    /// <summary>
    /// Used at runtime.
    /// </summary>
    public class SessionProgress
    {
        // All the properties that need to be saved...

        public ReactiveProperty<int> SampleValue;
        public ReactiveProperty<float> MusicVolume;
        public ReactiveProperty<float> SFXVolume;

        // .

        /// <summary>
        /// Creates new progress with default values.
        /// </summary>
        public SessionProgress()
        {
            SampleValue = new ReactiveProperty<int>(0);
            MusicVolume = new ReactiveProperty<float>(0.5f);
            SFXVolume = new ReactiveProperty<float>(0.5f);
        }

        /// <summary>
        /// Creates new progress from persistant data.
        /// </summary>
        /// <param name="progress"></param> Progress to initialize from
        public SessionProgress(PersistantProgress progress)
        {
            SampleValue = new ReactiveProperty<int>(progress.SampleValue);
            MusicVolume = new ReactiveProperty<float>();
            SFXVolume = new ReactiveProperty<float>();
        }
    }
}
