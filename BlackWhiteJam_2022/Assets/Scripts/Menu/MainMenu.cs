using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : SceneLoader
{
    [SerializeField] private GameObject _QuitButton;

    protected void Start()
    {
        if(Application.platform == RuntimePlatform.WebGLPlayer)
        {
            _QuitButton.SetActive(false);
        }
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
