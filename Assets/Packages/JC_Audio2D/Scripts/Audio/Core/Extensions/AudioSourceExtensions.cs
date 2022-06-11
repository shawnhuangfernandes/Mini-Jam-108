// Author:  Joseph Crump
// Date:    05/14/22
using UnityEngine;

namespace JC.Audio2D
{
    /// <summary>
    /// Extension methods for <see cref="AudioSource"/> components.
    /// </summary> 
    public static class AudioSourceExtensions
    {
        /// <summary>
        /// Sync the properties of an AudioSource to an 
        /// <see cref="GenericAudioSettings"/> object.
        /// </summary>
        /// <param name="settings">
        /// An object containing settings for an AudioSource.
        /// </param>
        /// <remarks>
        /// This operation can be leveraged to update the properties of
        /// audio sources during runtime when the settings on an
        /// <see cref="AudioEntry"/> are changed in the inspector.
        /// </remarks>
        public static void SyncToSettings(this AudioSource source, 
            GenericAudioSettings settings)
        {
            source.volume = settings.BaseVolume;
            source.pitch = settings.BasePitch;
            source.panStereo = settings.StereoPan;

            source.playOnAwake = settings.PlayOnAwake;
            source.loop = settings.Loop;

            source.outputAudioMixerGroup = settings.Mixer;
        }

        /// <summary>
        /// Create an interface wrapper for an AudioSource component.
        /// </summary>
        public static IAudioSource AsInterface(this AudioSource source)
        {
            return new AudioSourceLiteral(source);
        }
    }
}
