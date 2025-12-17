using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource sfxSource;

    public AudioClip footstepSFX;
    public AudioClip overworldMusic;
    public AudioClip caveMusic;

    public AudioClip[] variousSFX;

    void Awake()
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
    }

    void Start()
    {
        
    }

    public void PlayMusicSFX(AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.Play();
    }

    public void PlayMusic(AudioClip clip)
    {
        
    }

    public void PlayRandomSFX(params AudioClip[] clips)
    {
        int index = Random.Range(0, clips.Length);
        sfxSource.PlayOneShot(clips[index]);
    }

}
