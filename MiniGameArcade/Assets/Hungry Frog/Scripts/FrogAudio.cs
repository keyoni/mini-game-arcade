using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

//This was made using Brackey's very helpful sound manager tutorial
public class FrogAudio : MonoBehaviour
{
    public Sound[] sounds;

    void Awake() {
        Debug.Log("Audio is awake");
        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start() {
        //Debug.Log("Audio is starting");
        playSound("music");
    }

    public void playSound(string name) {
        Debug.Log("Playing sound: " + name);
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
}
