using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : SceneLoader
{
    [SerializeField] private GameObject _QuitButton;

    protected void Start()
    {
        /*if(Application.platform == RuntimePlatform.WebGLPlayer && _QuitButton != null)
        {
            _QuitButton.SetActive(false);
        }*/
        Time.timeScale = 1f;
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
