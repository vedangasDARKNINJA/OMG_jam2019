using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public Sound[] audioClips;

    public static AudioManager Instance{get;private set;}
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        foreach(Sound s in audioClips)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }

        Play("Music");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(audioClips, sound => sound.name == name);
        if(s==null)
        {
            return;
        }
        s.source.Play();
    }
}
