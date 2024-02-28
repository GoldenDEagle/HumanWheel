using System;

namespace Assets.Codebase.Models.Progress.Data
{
    /// <summary>
    /// Representation of ReactiveProgress that can be serialized and saved.
    /// </summary>
    [Serializable]
    public class PersistantProgress
    {
        // All the same properties as ReactiveProgress, but Serializable
        public int SampleValue;
        public float MusicVolume;
        public float SFXVolume;

        public void SetValues(SessionProgress progress)
        {
            // Fill all properties
            SampleValue = progress.SampleValue.Value;
            MusicVolume = progress.MusicVolume.Value;
            SFXVolume = progress.SFXVolume.Value;
        }
    }
}
