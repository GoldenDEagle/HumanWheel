using System.Collections.Generic;
using UnityEngine;

namespace Assets.Codebase.Data.Audio
{
    /// <summary>
    /// Holds all audioclips.
    /// </summary>
    [CreateAssetMenu(menuName = "Create Audio Data", fileName = "AudioDataContainer", order = 51)]
    public class AudioDataContainer : ScriptableObject
    {
        [SerializeField] private List<AudioClipWithId> _audioClips;

        public List<AudioClipWithId> AudioClips => _audioClips;
    }
}
