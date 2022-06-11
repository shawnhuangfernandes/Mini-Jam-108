// Author:  Joseph Crump
// Date:    05/14/22
using UnityEngine;

namespace JC.Audio2D
{
    /// <summary>
    /// Audio settings with extended parameters/defaults for SFX.
    /// </summary>
    [System.Serializable]
    public class SFXSettings : GenericAudioSettings
    {
        [Range(-3f, 0f)]
        public float RandomPitchDown = 0f;
        [Range(0f, 3f)]
        public float RandomPitchUp = 0f;

        /// <summary>
        /// Create an instance of the default settings.
        /// </summary>
        public static new SFXSettings Default()
        {
            return new SFXSettings
            {
                BaseVolume = 1f,
                BasePitch = 1f,
                StereoPan = 0f,

                RandomPitchDown = 0f,
                RandomPitchUp = 0f,

                PlayOnAwake = false,
                Loop = false
            };
        }
    }
}
