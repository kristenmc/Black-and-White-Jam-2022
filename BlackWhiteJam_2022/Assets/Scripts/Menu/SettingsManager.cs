using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Canvas _settingsCanvas;

    [SerializeField] private Settings _savedSettings;
    [SerializeField] private Settings _defaultSettings; 
    [SerializeField] private Slider _allSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

    private FMOD.Studio.VCA _vcaAll;
    private FMOD.Studio.VCA _vcaMusic;
    private FMOD.Studio.VCA _vcaSFX;

    private void Start()
    {
        _vcaAll = FMODUnity.RuntimeManager.GetVCA("vca:/All");
        _vcaMusic = FMODUnity.RuntimeManager.GetVCA("vca:/Music");
        _vcaSFX = FMODUnity.RuntimeManager.GetVCA("vca:/SFX");

        LoadPrefs();
    }

    public void CloseSettings()
    {
        SavePrefs();
        _settingsCanvas.enabled = false;
    }

    public void LoadPrefs()
    {
        Debug.Log("Load prefs");
        _allSlider.value = _savedSettings.AllVolume;
        _musicSlider.value = _savedSettings.MusicVolume;
        _sfxSlider.value = _savedSettings.SFXVolume;

    }

    public void SavePrefs()
    {
        _savedSettings.AllVolume = _allSlider.value;
        _savedSettings.MusicVolume = _musicSlider.value;
        _savedSettings.SFXVolume = _sfxSlider.value;
    }

    public void ResetPrefs()
    {
        _allSlider.value = _savedSettings.AllVolume = _defaultSettings.AllVolume;
        _musicSlider.value = _savedSettings.MusicVolume = _defaultSettings.MusicVolume;
        _sfxSlider.value = _savedSettings.SFXVolume = _defaultSettings.SFXVolume;
    }

    public void SetAllVolume(float volume)
    {
        _vcaAll.setVolume(volume); 
    }

    public void SetMusicVolume(float volume)
    {
        _vcaMusic.setVolume(volume); 
    }

    public void SetSFXVolume(float volume)
    {
        _vcaSFX.setVolume(volume); 
    }


}
