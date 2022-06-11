// Author:  Joseph Crump
// Date:    05/14/22
using UnityEngine;

namespace JC.Audio2D
{
    /// <summary>
    /// Audio settings with extended parameters/defaults for music.
    /// </summary>
    [System.Serializable]
    public class MusicSettings : GenericAudioSettings
    {
        [Min(0f)]
        [Tooltip("The duration used when fading in a track.")]
        public float FadeInDuration = 1f;

        [Min(0f)]
        [Tooltip("The duration used when fading out a track.")]
        public float FadeOutDuration = 1f;

        /// <summary>
        /// Create an instance of the default settings.
        /// </summary>
        public static new MusicSettings Default()
        {
            return new MusicSettings
            {
                BaseVolume = 1f,
                BasePitch = 1f,
                StereoPan = 0f,

                FadeInDuration = 1f,
                FadeOutDuration = 1f,

                PlayOnAwake = false,
                Loop = true
            };
        }
    }
}
