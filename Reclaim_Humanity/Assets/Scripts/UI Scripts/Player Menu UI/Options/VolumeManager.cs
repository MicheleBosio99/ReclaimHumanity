using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour {
    [SerializeField] private AudioMixer _audioMixer;
    
    [SerializeField] private Slider masterVolSlider;
    [SerializeField] private Slider musicVolSlider;
    [SerializeField] private Slider soundVolSlider;
    
    [SerializeField] private Toggle masterVolToggle;
    [SerializeField] private Toggle musicVolToggle;
    [SerializeField] private Toggle soundVolToggle;
    
    private float masterValue;
    private float musicValue;
    private float soundsValue;
    
    private bool masterToggleValue;
    private bool musicToggleValue;
    private bool soundsToggleValue;

    private const float TOLERANCE = 0.01f;
    
    private bool isChanging;

    private void Awake() { LoadAudioConfiguration(); }

    // MASTER
    public void HandleMasterVolumeSlider(float value) {
        isChanging = true;
        _audioMixer.SetFloat("MasterVolume", Mathf.Log10(value)*20f);
        masterToggleValue = Math.Abs(value - masterVolSlider.minValue) > TOLERANCE;
        masterVolToggle.isOn = masterToggleValue;
        isChanging = false;
        
        SaveAudioConfiguration();
    }
    
    public void HandleMasterVolumeToggle(bool active) {
        masterVolToggle.isOn = active;
        masterToggleValue = active;
        
        if (active) { if (!isChanging) { masterVolSlider.value = masterValue; } }
        else {
            var volRef = masterVolSlider.value; masterValue = volRef;
            masterVolSlider.value = masterVolSlider.minValue;
        }
    }
    
    // MUSIC
    public void HandleMusicVolumeSlider(float value) {
        isChanging = true;
        _audioMixer.SetFloat("MusicVolume", Mathf.Log10(value)*20f);
        musicToggleValue = Math.Abs(value - musicVolSlider.minValue) > TOLERANCE;
        musicVolToggle.isOn = musicToggleValue;
        isChanging = false;
        
        //SaveAudioConfiguration();
    }
    
    public void HandleMusicVolumeToggle(bool active) {
        musicVolToggle.isOn = active;
        musicToggleValue = active;
        
        if (active) { if (!isChanging) { musicVolSlider.value = musicValue; } }
        else {
            var volRef = musicVolSlider.value; musicValue = volRef;
            musicVolSlider.value = musicVolSlider.minValue;
            _audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolSlider.minValue)*20f);
        }
    }
    
    // SOUNDS
    public void HandleSoundsVolumeSlider(float value) {
        isChanging = true;
        _audioMixer.SetFloat("SoundsVolume", Mathf.Log10(value)*20f);
        soundsToggleValue = Math.Abs(value - soundVolSlider.minValue) > TOLERANCE;
        soundVolToggle.isOn = soundsToggleValue;
        isChanging = false;
        
        SaveAudioConfiguration();
    }
    
    public void HandleSoundsVolumeToggle(bool active) {
        soundVolToggle.isOn = active;
        soundsToggleValue = active;
        
        if (active) { if (!isChanging) { soundVolSlider.value = soundsValue; } }
        else {
            var volRef = soundVolSlider.value; soundsValue = volRef;
            soundVolSlider.value = soundVolSlider.minValue;
        }
    }

    private void LoadAudioConfiguration() {
        if (GameManager.volumeConfig == null) { masterValue = 1.0f; musicValue = 1.0f; soundsValue = 1.0f; return; }
        
        var volConfig = GameManager.volumeConfig.GetAllVolumes();
        
        masterVolToggle.isOn = volConfig[0] > TOLERANCE;
        masterVolSlider.value = volConfig[0];
        _audioMixer.SetFloat("MasterVolume", Mathf.Log10(masterVolSlider.value)*20f);
        
        musicVolToggle.isOn = volConfig[1] > TOLERANCE;
        musicVolSlider.value = volConfig[1];
        _audioMixer.SetFloat("MusicVolume", Mathf.Log10(masterVolSlider.value)*20f);
        
        soundVolToggle.isOn = volConfig[2] > TOLERANCE;
        soundVolSlider.value = volConfig[2];
        _audioMixer.SetFloat("SoundsVolume", Mathf.Log10(masterVolSlider.value)*20f);
    }
    
    private const float minDifferenceUpdate = 0.01f;
    private const float minTimeUpdate = 0.25f;
    private float lastTimeChanged;

    private void SaveAudioConfiguration() {
        if (Time.time - lastTimeChanged < minTimeUpdate) { return; }

        var volumes = GameManager.volumeConfig.GetAllVolumes();
        
        GameManager.volumeConfig.SetAllVolumes(new List<float>() {
            masterVolSlider.value,
            musicVolSlider.value,
            soundVolSlider.value});
        // if ((Mathf.Abs(volumes[0] - masterVolSlider.value) > minDifferenceUpdate) ||
        //         (Mathf.Abs(volumes[1] - musicVolSlider.value) > minDifferenceUpdate) ||
        //         (Mathf.Abs(volumes[2] - soundVolSlider.value) > minDifferenceUpdate)) {
        //     GameManager.volumeConfig.SetAllVolumes(new List<float>() {
        //         masterVolSlider.value,
        //         musicVolSlider.value,
        //         soundVolSlider.value
        //     });
        // }
        lastTimeChanged = 0.0f;
    }
}
