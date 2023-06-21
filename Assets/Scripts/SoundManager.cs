using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public SoundType[] Sounds;
    public AudioSource SoundEffect;
    public AudioSource SoundMusic;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayMusic(Sounds sound)
    {
        AudioClip clip = getSoundClip(sound);
        SoundMusic.clip = clip;
        SoundMusic.Play();
    }

    public void PlayEffect(Sounds sound)
    {
        AudioClip clip = getSoundClip(sound);
        if(clip!=null)
        {
            SoundEffect.PlayOneShot(clip);
        }
        else
        {
            Debug.Log("AudioClip of that type not found!");
        }
    }

    public void StopEffectPlay()
    {
        SoundEffect.Stop();
    }

    public AudioClip getSoundClip(Sounds sound)
    {
        SoundType item = Array.Find(Sounds, i => i.soundType == sound);
        if(item!=null)
        {
            return item.AudioClip;
        }
        return null;
    }
}
[Serializable]
public class SoundType
{
    public Sounds soundType;
    public AudioClip AudioClip;
}

public enum Sounds
{
    ButtonClick,
    LevelComplete,
    StartLevel,
    QuitGame,
    PlayerDied,
    PlayerMovement
}
