using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerManager : MonoBehaviour
{
   [SerializeField] private AudioMixer _audioMixer;

    public void SetMasterVolume(float level)
    {
        _audioMixer.SetFloat("MasterVolume", Mathf.Log10(level)*20f);
    }
    
    public void SetMusicVolume(float level)
    {
        _audioMixer.SetFloat("MusicVolume", Mathf.Log10(level)*20f);
    }
    
    public void SetSoundFXVolume(float level)
    {
        _audioMixer.SetFloat("SoundsVolume", Mathf.Log10(level)*20f);
    }
}

