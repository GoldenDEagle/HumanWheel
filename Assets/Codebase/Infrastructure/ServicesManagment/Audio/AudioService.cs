using Assets.Codebase.Data.Audio;
using Assets.Codebase.Infrastructure.ServicesManagment.Assets;
using Assets.Codebase.Models.Progress;
using System.Collections.Generic;
using UniRx;
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
        private AudioSource _effectsSource;
        private AudioSource _musicSource;

        // All clips loaded from container
        private Dictionary<SoundId, AudioClip> _clips;

        public AudioService(IAssetProvider assetProvider, IProgressModel progressModel, AudioSource effectsSource, AudioSource musicSource)
        {
            _assets = assetProvider;
            _progress = progressModel;
            _effectsSource = effectsSource;
            _musicSource = musicSource;

            InitData();
            progressModel.SessionProgress.SFXVolume.Subscribe(value => SetSFXVolume(value));
            progressModel.SessionProgress.MusicVolume.Subscribe(value => SetMusicVolume(value));
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
            _musicSource.volume = value;
        }

        public void SetSFXVolume(float value)
        {
            _effectsSource.volume = value;
        }

        // Next logic depends on project specifications.

        public void ChangeMusic(SoundId musicId)
        {
            _musicSource.clip = _clips[musicId];
        }

        public void PlaySfxSound(SoundId soundId)
        {
            _effectsSource.PlayOneShot(_clips[soundId]);
        }

        public void MuteAll()
        {
            AudioListener.pause = true;
        }

        public void UnmuteAll()
        {
            AudioListener.pause = false;
        }
    }
}
