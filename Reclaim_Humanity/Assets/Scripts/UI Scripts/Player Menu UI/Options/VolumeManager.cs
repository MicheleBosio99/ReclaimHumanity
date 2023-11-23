using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    
    [SerializeField] private Slider masterVolSlider;
    [SerializeField] private Slider musicVolSlider;
    [SerializeField] private Slider soundVolSlider;
    
    [SerializeField] private Toggle masterVolToggle;
    [SerializeField] private Toggle musicVolToggle;
    [SerializeField] private Toggle soundVolToggle;
    
    private bool isMasterActive = true;
    private bool isMusicActive = true;
    private bool isSoundActive = true;
    
    // TODO: system not properly working, does mute the volume but interaction slider/toggle are not working. Solve;
    
    // MASTER
    public void SetMasterVolume(float level) {
        _audioMixer.SetFloat("MasterVolume", Mathf.Log10(level)*20f);
        
        var active = !(level < 0.001);
        if (active ^ isMasterActive) return;
        masterVolToggle.isOn = active; isMasterActive = !isMasterActive;
    }

    public void ToggleMasterVolume(bool active) {
        _audioMixer.SetFloat("MasterVolume", active ? 0.0f : -80.0f);
        if (active ^ isMasterActive) return;
        masterVolSlider.value = active ? 1.0f : 1e-5f; isMasterActive = !isMasterActive;
    }

    // MUSIC
    public void SetMusicVolume(float level) {
        _audioMixer.SetFloat("MusicVolume", Mathf.Log10(level)*20f);
        
        var active = !(level < 0.001);
        if (active ^ isMusicActive) return;
        musicVolToggle.isOn = active; isMusicActive = !isMusicActive;
    }
    
    public void ToggleMusicVolume(bool active) {
        _audioMixer.SetFloat("MusicVolume", active ? 0.0f : -80.0f);
        
        if (active ^ isMusicActive) return;
        musicVolSlider.value = active ? 1.0f : 1e-5f; isMusicActive = !isMusicActive;
    }
    
    
    // SOUNDS
    public void SetSoundFXVolume(float level) {
        _audioMixer.SetFloat("SoundsVolume", Mathf.Log10(level)*20f);
        
        var active = !(level < 0.001);
        if (active ^ isSoundActive) return;
        soundVolToggle.isOn = active; isSoundActive = !isSoundActive;
    }
    
    public void ToggleSoundVolume(bool active) {
        _audioMixer.SetFloat("SoundsVolume", active ? 0.0f : -80.0f);
        
        if (active ^ isSoundActive) return;
        soundVolSlider.value = active ? 1.0f : 1e-5f; isSoundActive = !isSoundActive;
    }
}
