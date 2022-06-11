// Author:  Joseph Crump
// Date:    05/14/22
using UnityEngine;

namespace JC.Audio2D
{
    /// <summary>
    /// Contains serializable details that allows audio assets to be quickly 
    /// created and put to use in the editor.
    /// </summary>
    public abstract class AudioEntry : ScriptableObject
    {
        public abstract GenericAudioSettings Settings { get; }

        /// <summary>
        /// Generate a collection of audio sources from this audio entry's 
        /// variations.
        /// </summary>
        /// <param name="parent">
        /// The target GameObject for attaching the AudioSource component.
        /// </param>
        /// <returns>
        /// Returns a collection of audio sources.
        /// </returns>
        public abstract IAudioSource CreateAudioSource(GameObject parent);

        /// <summary>
        /// Play this audio source through the AudioManager.
        /// </summary>
        public void Play()
        {
            AudioManager.Instance.PlaySound(this);
        }

        /// <summary>
        /// Stop playing this audio source.
        /// </summary>
        public void Stop()
        {
            AudioManager.Instance.StopSound(this);
        }

        protected AudioSource CreateComponent(GameObject parent, AudioClip clip)
        {
            var source = parent.AddComponent<AudioSource>();
            source.clip = clip;
            source.SyncToSettings(Settings);

            return source;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            // Audio entries that PlayOnAwake can't be lazy initialized, so
            // they need to be added to the audio manager's serialized list
            if (Settings.PlayOnAwake)
                AudioManager.Instance.AddSerializedEntry(this);
        }
#endif
    }
}