using UnityEngine;

using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;   
    public AudioSource sfxSource;     

    [Header("Audio Clips")]
    public List<AudioClip> musicClips; 
    public List<AudioClip> sfxClips;    

    [Header("Settings")]
    [Range(0f, 1f)] public float musicVolume = 1f;
    [Range(0f, 1f)] public float sfxVolume = 1f;

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        musicSource.volume = musicVolume;
        sfxSource.volume = sfxVolume;
    }

   
    public void PlayMusic(string clipName, bool loop = true)
    {
        AudioClip clip = musicClips.Find(c => c.name == clipName);
        if (clip == null)
        {
            Debug.LogWarning($"Music clip {clipName} not found!");
            return;
        }

        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
    }
    
    public void PlaySFX(string clipName)
    {
        AudioClip clip = sfxClips.Find(c => c.name == clipName);
        if (clip == null)
        {
            Debug.LogWarning($"SFX clip {clipName} not found!");
            return;
        }

        sfxSource.PlayOneShot(clip, sfxVolume);
    }
    
    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        musicSource.volume = musicVolume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        sfxSource.volume = sfxVolume;
    }
}

