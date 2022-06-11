// Author:  Joseph Crump
// Date:    05/14/22
using System.Collections;
using UnityEngine;

namespace JC.Audio2D
{
    /// <summary>
    /// Wrapper for an audio source that plays music.
    /// </summary>
    public class MusicTrack : AudioSourceLiteral
    {
        public bool IsMuted { get => source.mute; }

        private MusicSettings settings;

        private Coroutine fadeCoroutine;

        public MusicTrack(AudioSource source, MusicSettings settings) : base(source)
        {
            this.source = source;
            this.settings = settings;
        }

        public override void Play()
        {
            if (IsMuted)
            {
                Debug.LogWarning(
                    $"Attempting to play a muted music track. Make sure to " +
                    $"unmute the track first.");

                return;
            }

            source.SyncToSettings(settings);
            base.Play();
        }

        /// <summary>
        /// Mute/Unmute the track.
        /// </summary>
        public void Mute(bool value)
        {
            source.mute = value;
        }

        /// <summary>
        /// Mute the track.
        /// </summary>
        public void Mute()
        {
            Mute(true);
        }

        /// <summary>
        /// Unmute the track.
        /// </summary>
        public void Unmute()
        {
            Mute(false);
        }

        /// <summary>
        /// Unmute and fade in the track.
        /// </summary>
        public void FadeIn()
        {
            if (!IsMuted)
                return;

            if (fadeCoroutine != null)
                AudioManager.StopCoroutine(fadeCoroutine);

            fadeCoroutine = AudioManager.StartCoroutine(
                FadeIn_Coroutine(settings.FadeInDuration));
        }

        /// <summary>
        /// Fade out and mute the track.
        /// </summary>
        public void FadeOut()
        {
            if (IsMuted)
                return;

            if (fadeCoroutine != null)
                AudioManager.StopCoroutine(fadeCoroutine);

            fadeCoroutine = AudioManager.StartCoroutine(
                FadeOut_Coroutine(settings.FadeInDuration));
        }

        private IEnumerator FadeIn_Coroutine(float duration)
        {
            Unmute();

            yield return FadeVolume(duration, settings.BaseVolume);
        }

        private IEnumerator FadeOut_Coroutine(float duration)
        {
            yield return FadeVolume(duration, 0f);

            Mute();
        }

        private IEnumerator FadeVolume(float duration, float targetVolume)
        {
            float startVolume = source.volume;

            var yieldInstruction = new WaitForEndOfFrame();

            float timer = 0f;
            while (timer < duration)
            {
                timer += Time.deltaTime;

                float interpolant = timer / duration;

                source.volume = Mathf.Lerp(startVolume, targetVolume, interpolant);

                yield return yieldInstruction;
            }
        }
    }
}
