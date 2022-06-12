// Author:  Joseph Crump
// Date:    01/31/22

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JC.Audio2D
{
    /// <summary>
    /// Singleton object used to play audio clips from anywhere in the project.
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Collection of audio entries belonging to the audio manager.")]
        private List<AudioEntry> audioEntries = new List<AudioEntry>();

        private Dictionary<AudioEntry, IAudioSource> audioDictionary
            = new Dictionary<AudioEntry, IAudioSource>();

        private static readonly object instanceLock = new object();
        private static AudioManager _instance;
        public static AudioManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (instanceLock)
                    {
                        GetOrCreateInstance();
                    }
                }

                return _instance;
            }
        }

        private List<Soundtrack> activeSoundtracks = new List<Soundtrack>();

        private void Awake()
        {
            EnforceSingleton();
            Initialize();
        }

        private void Initialize()
        {
            // Initialize the AudioSources Dictionary
            audioDictionary = new Dictionary<AudioEntry, IAudioSource>();

            foreach (var entry in audioEntries)
            {
                AddDictionaryEntry(entry);
            }
        }

        /// <summary>
        /// Play an audio entry.
        /// </summary>
        public void PlaySound(AudioEntry entry)
        {
            if (entry is Soundtrack soundtrack)
                HandleSoundtrackStarted(soundtrack);

            var source = GetOrAddAudioSource(entry);
            source.Play();
        }

        /// <summary>
        /// Stop an audio entry that is currently playing.
        /// </summary>
        public void StopSound(AudioEntry entry)
        {
            if (entry == null)
                return;

            if (entry is Soundtrack soundtrack)
                HandleSoundtrackStopped(soundtrack);

            IAudioSource sourceGroup = GetOrAddAudioSource(entry);
            sourceGroup.Stop();
        }

        /// <summary>
        /// Stop a soundtrack. Works the same as StopSound but fades out 
        /// instead of stopping instantly.
        /// </summary>
        public void FadeInSoundtrack(Soundtrack soundtrack)
        {
            HandleSoundtrackStarted(soundtrack);

            var audiosource = audioDictionary[soundtrack] as SoundtrackSourceGroup;
            audiosource.FadeIn();
        }

        /// <summary>
        /// Stop a soundtrack. Works the same as StopSound but fades out 
        /// instead of stopping instantly.
        /// </summary>
        public void FadeOutSoundtrack(Soundtrack soundtrack)
        {
            HandleSoundtrackStopped(soundtrack);

            var audiosource = audioDictionary[soundtrack] as SoundtrackSourceGroup;
            audiosource.FadeOut();
        }

        /// <summary>
        /// Stop any soundtracks that are playing and start playing a 
        /// different soundtrack.
        /// </summary>
        public void ChangeSoundtrack(Soundtrack soundtrack)
        {
            activeSoundtracks.ForEach(s => FadeOutSoundtrack(s));
            activeSoundtracks.Clear();

            FadeInSoundtrack(soundtrack);
        }

        /// <summary>
        /// Mute a track from a soundtrack.
        /// </summary>
        public void MuteMusicTrack(Soundtrack soundtrack, int trackIndex)
        {
            var audiosource = audioDictionary[soundtrack] as SoundtrackSourceGroup;
            audiosource.MuteTrack(trackIndex);
        }

        /// <summary>
        /// Unmute a track from a soundtrack.
        /// </summary>
        public void UnmuteMusicTrack(Soundtrack soundtrack, int trackIndex)
        {
            var audiosource = audioDictionary[soundtrack] as SoundtrackSourceGroup;
            audiosource.UnmuteTrack(trackIndex);
        }

        private void HandleSoundtrackStarted(Soundtrack soundtrack)
        {
            if (activeSoundtracks.Contains(soundtrack))
                return;

            activeSoundtracks.Add(soundtrack);
        }

        private void HandleSoundtrackStopped(Soundtrack soundtrack)
        {
            if (!activeSoundtracks.Contains(soundtrack))
                return;

            activeSoundtracks.Remove(soundtrack);
        }

#if UNITY_EDITOR
        internal void AddSerializedEntry(AudioEntry entry)
        {
            if (audioEntries.Contains(entry))
                return;

            audioEntries.Add(entry);
        }
#endif

        private void AddDictionaryEntry(AudioEntry entry)
        {
            var key = entry;
            var sourceGroup = entry.CreateAudioSource(gameObject);

            audioDictionary.Add(key, sourceGroup);

            if (entry.Settings.PlayOnAwake)
            {
                PlaySound(entry);
            }
        }

        private IAudioSource GetOrAddAudioSource(AudioEntry entry)
        {
            if (!audioDictionary.ContainsKey(entry))
                AddDictionaryEntry(entry);

            var sourceGroup = audioDictionary[entry];

            return sourceGroup;
        }

        private void EnforceSingleton()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        private static void GetOrCreateInstance()
        {
            if (_instance != null)
                return;

            var instances = FindObjectsOfType<AudioManager>().ToList();

            if (instances.Count == 1)
            {
                _instance = instances[0];
            }
            else if (instances.Count > 1)
            {
                throw new System.Exception(
                    $"More than one instance of {nameof(AudioManager)} exists. " +
                    $"Ensure that only one instance exists at one time.");
            }
            else
            {
                _instance = Instantiate(new GameObject(nameof(AudioManager))).AddComponent<AudioManager>();
            }

            if (!Application.isPlaying)
                return;

            DontDestroyOnLoad(_instance.gameObject);
        }

        private static void Delete(Object obj)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.delayCall += () => DestroyImmediate(obj);
#else
            Destroy(obj);
#endif
        }
    }
}