// Author:  Joseph Crump
// Date:    05/14/22
using UnityEngine;

namespace JC.Audio2D
{
    /// <summary>
    /// Wrapper for an audio source with no extra functionality.
    /// </summary>
    public class AudioSourceLiteral : IAudioSource
    {
        protected AudioManager AudioManager => AudioManager.Instance;

        protected AudioSource source;

        public AudioSourceLiteral(AudioSource source)
        {
            this.source = source;
        }

        public virtual bool IsPlaying() => source.isPlaying;
        public virtual void Play() => source.Play();
        public virtual void SetVolume(float volume) => source.volume = volume;
        public virtual void Stop() => source.Stop();

        public static implicit operator AudioSource(AudioSourceLiteral literal)
        {
            return literal.source;
        }

        public static implicit operator AudioSourceLiteral(AudioSource source)
        {
            return new AudioSourceLiteral(source);
        }
    }
}
