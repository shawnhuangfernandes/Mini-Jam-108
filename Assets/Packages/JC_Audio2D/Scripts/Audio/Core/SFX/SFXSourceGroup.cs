// Author:  Joseph Crump
// Date:    01/31/22
using System;
using UnityEngine;

namespace JC.Audio2D
{
    /// <summary>
    /// A collection of audio sources that can be used interchangeably. Allows for
    /// sounds with the same tag to have variations.
    /// </summary>
    [Serializable]
    public class SFXSourceGroup : AudioSourceCollection<SFXAudioSource>
    {
        /// <summary>
        /// Play a random audio source from this collection.
        /// </summary>
        public override void Play()
        {
            int random = UnityEngine.Random.Range(0, audioSources.Count);
            Play(random);
        }

        /// <summary>
        /// Play a specific audio source from this collection.
        /// </summary>
        /// <param name="index">
        /// The audio source index to play.
        /// </param>
        private void Play(int index)
        {
            bool indexIsValid = TryGetSource(index, out var source);

            if (!indexIsValid)
                return;

            source.Play();
        }
    }
}