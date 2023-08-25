using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public static class SoundManager
{
    private static EventInstance _musicInstance;
    
    public static void PlayMusic(EventReference sound)
    {
        _musicInstance = RuntimeManager.CreateInstance(sound);
        _musicInstance.start();
    }
    
    public static void PlaySound(EventReference sound)
    {
        PlaySound(sound, Vector3.zero);
    }
    
    public static void PlaySound(EventReference sound, Vector3 location)
    {
        RuntimeManager.PlayOneShot(sound, location);
    }
}
