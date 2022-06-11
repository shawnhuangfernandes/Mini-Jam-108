// Author:  Joseph Crump
// Date:    05/14/22
using UnityEngine;

namespace JC.Audio2D
{
    /// <summary>
    /// Type of audio source that uses settings to play with specific 
    /// variations.
    /// </summary>
    public class SFXAudioSource : AudioSourceLiteral
    {
        private SFXSettings settings;

        public SFXAudioSource(AudioSource source, SFXSettings settings) : base(source)
        {
            this.source = source;
            this.settings = settings;
        }

        public override void Play()
        {
            source.SyncToSettings(settings);

            // Calculate pitch variance
            float pitchDown = settings.RandomPitchDown;
            float pitchUp = settings.RandomPitchUp;
            float pitchVariance = Random.Range(pitchDown, pitchUp);

            // Modify source pitch
            source.pitch = settings.BasePitch + pitchVariance;

            source.Play();
        }
    }
}
