﻿using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public int level;
    public Sound[] sounds;

    private void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        s.source.Play();
    }

    private void Start()
    {
        switch (level)
        {
            case 1:
                Play("AmbientMusic");
                Play("ForestAmbientSound");
                break;
            case 2:
                Play("AmbientMusic");
                Play("ForestAmbientSound");
                break;
            case 3:
                Play("AmbientMusic");
                Play("AmbientLavaSound");
                break;
            default:
                Play("AmbientMusic");
                break;

        }
    }
}
