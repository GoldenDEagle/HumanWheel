using System;
using UnityEngine;

namespace Assets.Codebase.Data.Audio
{
    [Serializable]
    public class AudioClipWithId
    {
        public SoundId Id;
        public AudioClip Clip;
    }
}
