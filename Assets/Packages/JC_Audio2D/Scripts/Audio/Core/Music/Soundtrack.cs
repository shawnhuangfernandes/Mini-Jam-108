// Author:  Joseph Crump
// Date:    05/14/22
using System.Collections.Generic;
using UnityEngine;

namespace JC.Audio2D
{
    /// <summary>
    /// Asset describing the parameters of an audio soundtrack.
    /// </summary>
    [CreateAssetMenu(
        fileName = "NewSoundtrack", 
        menuName = "Audio/Soundtrack",
        order = AssetMenuPriority.Soundtrack)]
    public class Soundtrack : AudioEntry
    {
        public override GenericAudioSettings Settings => settings;

        [SerializeField]
        [Tooltip("Settings for the Audio Source components created from this entry.")]
        private MusicSettings settings = MusicSettings.Default();

        [SerializeField]
        [Tooltip("The layers that make up the soundtrack.")]
        private List<Track> tracks = new List<Track>();

        public override IAudioSource CreateAudioSource(GameObject parent)
        {
            var sourceGroup = new SoundtrackSourceGroup();

            foreach (var track in tracks)
            {
                var component = CreateComponent(parent, track.Clip);
                var musicTrack = new MusicTrack(component, settings);

                if (!track.PlayInitially)
                    musicTrack.Mute();

                sourceGroup.Add(musicTrack);
            }

            return sourceGroup;
        }

        /// <summary>
        /// Mute a track from the soundtrack.
        /// </summary>
        public void MuteTrack(int trackIndex)
        {
            AudioManager.Instance.MuteMusicTrack(soundtrack: this, trackIndex);
        }

        /// <summary>
        /// Unmute a track from the soundtrack.
        /// </summary>
        public void UnmuteTrack(int trackIndex)
        {
            AudioManager.Instance.UnmuteMusicTrack(soundtrack: this, trackIndex);
        }
    }

    /// <summary>
    /// A track that represents a layer in a <see cref="Soundtrack"/>.
    /// </summary>
    [System.Serializable]
    public class Track
    {
        [SerializeField]
        [Tooltip("The audio clip that will be played by this the AudioSource.")]
        private AudioClip _clip;
        public AudioClip Clip => _clip;

        [SerializeField]
        [Tooltip(
            "If false, the track won't play unless explicitly unmuted. " +
            "Useful for starting a soundtrack with dynamic reorchestration.")]
        private bool _playInitially = true;
        public bool PlayInitially { get => _playInitially; }
    }
}
