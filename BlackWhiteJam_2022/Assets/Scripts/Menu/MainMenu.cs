using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : SceneLoader
{
    [SerializeField] private SettingsManager _settings;
    [SerializeField] private GameObject _QuitButton;

    protected void Start()
    {
        Debug.Log(Application.platform);
        if(Application.platform == RuntimePlatform.WebGLPlayer)
        {
            _QuitButton.SetActive(false);
        }
        _settings.LoadPrefs();
    }

    public void OpenCanvas(Canvas canvas)
    {
        canvas.enabled = true;
    }

    public void CloseCanvas(Canvas canvas)
    {
        canvas.enabled = false;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
