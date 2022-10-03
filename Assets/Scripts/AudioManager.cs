using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    static public AudioManager DO;
 

    void Awake()
    {
        DO = this;

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
                
        s.source.Play();
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Stop();
    }
    public void VolumeDown(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume -= 0.3f * Time.deltaTime;
        if (s.source.volume <= 0)
        {
            s.source.Stop();
        }
    }
    public void VolumeFade(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume -= 0.3f * Time.deltaTime;
    }
    public void VolumeUp(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        } 
        if (s.source.volume <= 0.7f)
        {
            s.source.volume += 0.35f * Time.deltaTime;
        }
    }
    public void VolumeIn(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.volume = 0.02f;
        }
        public void VolumeOut(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
           if (s == null)
           {
               Debug.LogWarning("Sound: " + name + " not found!");
               return;
           }
            s.source.volume = 0;
}
    }


    //Como llamarlo FindObjectOfType<AudioManager>().Play("NOMBREDESONIDO");

