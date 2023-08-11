using System;
using AYellowpaper.SerializedCollections;
using CustomEvent;
using UnityEngine;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource bgmSource;
        [SerializeField] private SerializedDictionary<string, AudioClip> clipDictionary;

        public static readonly Evt<string> OnPlaySfx = new Evt<string>();
        public static readonly Evt<string> OnPlayBgm = new Evt<string>();
        public static readonly Evt OnStopMusic = new Evt(); 

        private void Awake()
        {
            OnPlaySfx.AddListener(PlaySfx);
            OnPlayBgm.AddListener(PlayBgm);
            OnStopMusic.AddListener(StopMusic);
        }

        private void OnDestroy()
        {
            OnPlaySfx.RemoveListener(PlaySfx);
            OnPlayBgm.RemoveListener(PlayBgm);
            OnStopMusic.RemoveListener(StopMusic);
        }
        
        private void PlayBgm(string id)
        {
            if(!clipDictionary.TryGetValue(id, out var clip)) return;
            
            bgmSource.clip = clip;
            bgmSource.Play();
        }
        
        private void PlaySfx(string id)
        {
            if(!clipDictionary.TryGetValue(id, out var clip)) return;
            
            sfxSource.PlayOneShot(clip);
        }
        
        private void StopMusic()
        {
            bgmSource.Stop();
        }
    }
}
