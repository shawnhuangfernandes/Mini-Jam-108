// Author:  Joseph Crump
// Date:    05/14/22
using UnityEngine;
using UnityEngine.Audio;

namespace JC.Audio2D
{
    /// <summary>
    /// Object containing info for an <see cref="AudioSource"/>.
    /// </summary>
    [System.Serializable]
    public class GenericAudioSettings
    {
        [Range(0f, 1f)]
        public float BaseVolume = 1f;
        [Range(-3f, 3f)]
        public float BasePitch = 1f;
        [Range(-1f, 1f)]
        public float StereoPan = 0f;

        public bool PlayOnAwake = false;
        public bool Loop = false;
        public AudioMixerGroup Mixer;

        /// <summary>
        /// Create an instance of the default settings.
        /// </summary>
        public static GenericAudioSettings Default()
        {
            return new GenericAudioSettings
            {
                BaseVolume = 1f,
                BasePitch = 1f,
                StereoPan = 0f,

                PlayOnAwake = false,
                Loop = false
            };
        }
    }
}
