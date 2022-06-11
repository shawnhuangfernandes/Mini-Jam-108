// Author:  Joseph Crump
// Date:    05/14/22

namespace JC.Audio2D
{
    /// <summary>
    /// Interface for interacting with an AudioSource.
    /// </summary>
    public interface IAudioSource
    {
        void Play();
        void Stop();
        bool IsPlaying();
        void SetVolume(float volume);
    }
}
