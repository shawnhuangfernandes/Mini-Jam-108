// Author:  Joseph Crump
// Date:    05/14/22
using System;
using System.Collections.Generic;

namespace JC.Audio2D
{
    public abstract class AudioSourceCollection<TAudioSource> : IAudioSource
        where TAudioSource : IAudioSource
    {
        protected List<TAudioSource> audioSources = new List<TAudioSource>();

        /// <summary>
        /// Add an audio source component to the SFX group.
        /// </summary>
        public void Add(TAudioSource source)
        {
            if (audioSources.Contains(source))
                return;

            audioSources.Add(source);
            OnSourceAdded(source);
        }

        protected virtual void OnSourceAdded(TAudioSource source) { }

        /// <summary>
        /// Play a random audio source from this collection.
        /// </summary>
        public abstract void Play();

        /// <summary>
        /// Stop all audio sources in this group that are currently playing.
        /// </summary>
        public virtual void Stop()
        {
            // Stop all sources in the group
            foreach (IAudioSource source in audioSources)
            {
                if (source.IsPlaying())
                    source.Stop();
            }
        }

        /// <summary>
        /// Evaluate whether any of the audio sources are playing.
        /// </summary>
        public virtual bool IsPlaying()
        {
            foreach (IAudioSource source in audioSources)
            {
                if (source.IsPlaying())
                    return true;
            }

            return false;
        }

        public void SetVolume(float volume)
        {
            audioSources.ForEach(a => a.SetVolume(volume));
        }

        protected bool TryGetSource(int index, out TAudioSource source)
        {
            source = default;

            try
            {
                source = audioSources[index];
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }

            return true;
        }
    }
}