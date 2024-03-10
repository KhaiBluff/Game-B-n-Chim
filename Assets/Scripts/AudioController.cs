using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AudioController : MonoBehaviour
{
    [Header("Main Settings:")]
    [Range(0, 1)]
    public float musicVolume;
    [Range(0, 1)]
    public float sfxVolume;
    public AudioSource musicAus;
    public AudioSource sfxAus;
    [Header("Game Sound And Musics:")]
    public AudioClip shooting;
    public AudioClip win;
    public AudioClip lose;
    public AudioClip bgmusics;

    public void playSound(AudioClip sound, AudioSource aus = null)
    {
        if (!aus)
        {
            aus = sfxAus;
        }
        if (aus)
        {
            aus.PlayOneShot(sound, sfxVolume);
        }
    }
    public void playSound(AudioClip[] sounds, AudioSource aus = null)
    {
        if (!aus)
        {
            aus = sfxAus;
        }
        if (aus)
        {
            int randIdx = Random.Range(0, sounds.Length);
            aus.PlayOneShot(sounds[randIdx], sfxVolume);
        }
    }
    public void playMusic(AudioClip music, bool loop = true)
    {
        if (musicAus)
        {
            musicAus.clip = music;
            musicAus.loop = loop;
            musicAus.volume = musicVolume;
            musicAus.Play();
        }
    }
    public void playMusic(AudioClip[] musics, bool loop = true)
    {
        if (musicAus)
        {
            int randIdx = Random.Range(0, musics.Length);
            if (musics[randIdx] != null)
            {
                musicAus.clip = musics[randIdx];
                musicAus.loop = loop;
                musicAus.volume = musicVolume;
                musicAus.Play();
            }
        }
    }
    public void Start()
    {
        playMusic(bgmusics);
    }
}