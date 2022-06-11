// Author:  Joseph Crump
// Date:    01/31/22
using UnityEngine;

namespace JC.Audio2D
{
    /// <summary>
    /// ScriptableObject that can be accessed by objects in the scene in order 
    /// to utilize the AudioManager. Allows assets to have remote access to
    /// the AudioManager.
    /// </summary>
    [CreateAssetMenu(
        fileName = "AudioFacilitator", 
        menuName = "Audio/Facilitator", 
        order = AssetMenuPriority.AudioFacilitator)]
    public class AudioFacilitator : ScriptableObject
    {
        private AudioManager AudioManager => AudioManager.Instance;

        /// <summary>
        /// Play an audio entry.
        /// </summary>
        public void PlaySound(AudioEntry entry)
        {
            AudioManager.PlaySound(entry);
        }

        /// <summary>
        /// Stop playing an audio entry.
        /// </summary>
        public void StopSound(AudioEntry entry)
        {
            AudioManager.StopSound(entry);
        }

        /// <summary>
        /// Fade in a music soundtrack.
        /// </summary>
        public void FadeInSoundtrack(Soundtrack soundtrack)
        {
            AudioManager.FadeInSoundtrack(soundtrack);
        }

        /// <summary>
        /// Fade out a music soundtrack.
        /// </summary>
        public void FadeOutSoundtrack(Soundtrack soundtrack)
        {
            AudioManager.FadeOutSoundtrack(soundtrack);
        }

        /// <summary>
        /// Crossfades to a new music soundtrack, stopping any soundtrack that
        /// is currently playing.
        /// </summary>
        public void ChangeSoundtrack(Soundtrack soundtrack)
        {
            AudioManager.ChangeSoundtrack(soundtrack);
        }
    }
}