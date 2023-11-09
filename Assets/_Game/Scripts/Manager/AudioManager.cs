using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] List<Sound> musicSounds;
    [SerializeField] List<Sound> sfxSounds;
    [SerializeField] AudioSource musicSource, sfxSource;

    private void Start() 
    {
        PlayMusic("MainMenu");
    }

    public void PlayMusic(string name)
    {
        Sound s = musicSounds.Find(x => x.name == name);

        if (s == null)
        {
            return;
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = sfxSounds.Find(x => x.name == name);

        if (s == null)
        {
            return;
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
