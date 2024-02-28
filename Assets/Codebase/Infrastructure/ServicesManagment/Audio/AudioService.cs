using Assets.Codebase.Data.Audio;
using Assets.Codebase.Infrastructure.ServicesManagment.Assets;
using Assets.Codebase.Models.Progress;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Codebase.Infrastructure.ServicesManagment.Audio
{
    /// <summary>
    /// Implementation which loads sounds through asset provider.
    /// </summary>
    public class AudioService : IAudioService
    {
        private const string AudioPath = "Audio/AudioDataContainer";

        private IAssetProvider _assets;
        private IProgressModel _progress;

        // All clips loaded from container
        private Dictionary<SoundId, AudioClip> _clips;

        public AudioService(IAssetProvider assetProvider, IProgressModel progressModel)
        {
            _assets = assetProvider;
            _progress = progressModel;

            InitData();
        }

        private void InitData()
        {
            var audioData = _assets.LoadResource<AudioDataContainer>(AudioPath);

            if (audioData.AudioClips == null) return;

            _clips = new Dictionary<SoundId, AudioClip>();
            foreach (var clip in audioData.AudioClips)
            {
                _clips.Add(clip.Id, clip.Clip);
            }
        }

        public void SetMusicVolume(float value)
        {
            _progress.SessionProgress.MusicVolume.Value = value;
        }

        public void SetSFXVolume(float value)
        {
            _progress.SessionProgress.SFXVolume.Value = value;
        }

        // Next logic depends on project specifications.

        public void ChangeMusic(SoundId musicId)
        {
            throw new System.NotImplementedException();
        }

        public void PlaySfxSound(SoundId soundId)
        {
            throw new System.NotImplementedException();
        }

        public void MuteAll()
        {
            throw new System.NotImplementedException();
        }

        public void UnmuteAll()
        {
            throw new System.NotImplementedException();
        }
    }
}
