// Author:  Joseph Crump
// Date:    05/14/22
using System.Collections.Generic;

namespace JC.Audio2D
{
    /// <summary>
    /// Audio source group tailored towards playing soundtracks.
    /// </summary>
    public class SoundtrackSourceGroup : AudioSourceCollection<MusicTrack>
    {
        private bool isPlaying = false;

        /// <summary>
        /// Play all audio sources in the soundtrack.
        /// </summary>
        public override void Play()
        {
            if (isPlaying)
                return;

            foreach (var source in audioSources)
            {
                source.Play();
            }

            isPlaying = true;
        }

        public override void Stop()
        {
            // stop all tracks in the soundtrack
            base.Stop();

            isPlaying = false;
        }

        /// <summary>
        /// Fade in the active tracks in this soundtrack.
        /// </summary>
        public void FadeIn()
        {
            foreach (var source in audioSources)
            {
                if (source.IsMuted)
                    continue;

                source.FadeIn();
            }
        }

        /// <summary>
        /// Fade out the active tracks in this soundtrack.
        /// </summary>
        public void FadeOut()
        {
            foreach (var source in audioSources)
            {
                if (source.IsMuted)
                    continue;

                source.FadeOut();
            }
        }

        /// <summary>
        /// Set the volume of a track to 0, but don't stop playing it.
        /// </summary>
        /// <param name="trackIndex">
        /// The index of the track in the inspector.
        /// </param>
        public void MuteTrack(int trackIndex)
        {
            bool indexIsValid = TryGetSource(trackIndex, out var track);

            if (!indexIsValid)
                return;

            track.Mute();
        }

        /// <summary>
        /// Restore the volume of a muted track.
        /// </summary>
        /// <param name="trackIndex">
        /// The index of the track in the inspector.
        /// </param>
        public void UnmuteTrack(int trackIndex)
        {
            bool indexIsValid = TryGetSource(trackIndex, out var track);

            if (!indexIsValid)
                return;

            track.Unmute();
        }
    }
}
