using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreMainMenu : SceneLoader
{
    [SerializeField] private string[] _banks;
    [SerializeField] private GameObject _button;

    private void Awake()
    {
        _button.SetActive(false);
        LoadBanks();
    }
    private void LoadBanks()
    {
        foreach (string b in _banks)
        {
            FMODUnity.RuntimeManager.LoadBank(b, true);
            Debug.Log("Loaded bank " + b);
        }
        
        //    For most browsers / WebGL.  Reset audio on response to user interaction (LoadBanks is called from a button press), to allow audio to be heard.
        
        FMODUnity.RuntimeManager.CoreSystem.mixerSuspend();
        FMODUnity.RuntimeManager.CoreSystem.mixerResume();
        StartCoroutine(CheckBanksLoaded());
    }

    IEnumerator CheckBanksLoaded()
    {
        while (!AllBanksLoaded())
        {
            yield return null;
        }

        _button.SetActive(true);
    }

    public bool AllBanksLoaded()
    {
        foreach (string b in _banks)
        {
            if(!FMODUnity.RuntimeManager.HasBankLoaded(b))
            {
                return false;
            }
        }
        return true;
    }
    
}
