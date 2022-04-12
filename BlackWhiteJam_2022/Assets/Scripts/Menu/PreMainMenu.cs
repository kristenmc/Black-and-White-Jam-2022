using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PreMainMenu : SceneLoader
{
    [SerializeField] private string[] _banks;
    [SerializeField] private GameObject _buttonStart;
    [SerializeField] private GameObject _loadBanks;
    [SerializeField] private TextMeshProUGUI _text;

    private void Awake()
    {
        _buttonStart.SetActive(false);
        _loadBanks.SetActive(true);

    }
    public void LoadBanks()
    {
        _text.text = "Waiting for Banks to Load";
        foreach (string b in _banks)
        {
            FMODUnity.RuntimeManager.LoadBank(b, true);
            Debug.Log("Loaded bank " + b);
        }
        
        //    For most browsers / WebGL.  Reset audio on response to user interaction (LoadBanks is called from a button press), to allow audio to be heard.
        
        FMODUnity.RuntimeManager.CoreSystem.mixerSuspend();
        FMODUnity.RuntimeManager.CoreSystem.mixerResume();
        _text.text = "Checking if banks loaded";
        StartCoroutine(CheckBanksLoaded());
    }

    IEnumerator CheckBanksLoaded()
    {
        while (!AllBanksLoaded())
        {
            yield return null;
        }

        _loadBanks.SetActive(false);
        _buttonStart.SetActive(true);
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
