// Author:  Joseph Crump
// Date:    05/14/22
using System.Collections.Generic;
using UnityEngine;

namespace JC.Audio2D
{
    /// <summary>
    /// Type of AudioEntry suitable for playing SFX.
    /// </summary>
    [CreateAssetMenu(
        fileName = "NewSFX", 
        menuName = "Audio/SFX",
        order = AssetMenuPriority.SFX)]
    public class SoundEffect : AudioEntry
    {
        public override GenericAudioSettings Settings => settings;

        [SerializeField]
        [Tooltip("Settings for the Audio Source component created from this entry.")]
        private SFXSettings settings = SFXSettings.Default();

        [SerializeField]
        [Tooltip("Random ariations of the sound effect.")]
        private List<SFXVariation> variations = new List<SFXVariation>();

        public override IAudioSource CreateAudioSource(GameObject parent)
        {
            var sourceGroup = new SFXSourceGroup();

            foreach (var variation in variations)
            {
                var component = CreateComponent(parent, variation.Clip);
                var sfxSource = new SFXAudioSource(component, settings);
                sourceGroup.Add(sfxSource);
            }

            return sourceGroup;
        }
    }

    /// <summary>
    /// Contains serializable details for a single audio source that can be
    /// created at runtime. An AudioEntry can have any number of these.
    /// </summary>
    [System.Serializable]
    public class SFXVariation
    {
        [SerializeField]
        [Tooltip("The audio clip that will be played by this the AudioSource.")]
        private AudioClip _clip;
        public AudioClip Clip { get => _clip; }
    }
}
