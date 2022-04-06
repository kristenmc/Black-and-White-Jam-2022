using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] MainMenu _menuManager;
    public void CloseSettings()
    {
        Debug.Log("Close Settings Panel");
    }

    public void LoadPrefs()
    {
        Debug.Log("Load Settings");
    }

    public void SavePrefs()
    {
        Debug.Log("Save Settings");
    }

    public void ResetPrefs()
    {
        Debug.Log("Reset Settings");
    }

    public void SetAllVolume(float volume)
    {
        Debug.Log("Set All Volume: " + volume);
    }

    public void SetMusicVolume(float volume)
    {
        Debug.Log("Set Music Volume: " + volume);
    }

    public void SetSFXVolume(float volume)
    {
        Debug.Log("Set SFX Volume: " + volume);
    }


}
