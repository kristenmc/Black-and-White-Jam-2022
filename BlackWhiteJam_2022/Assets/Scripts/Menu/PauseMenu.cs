using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MainMenu
{
    [SerializeField] private Canvas _pauseMenuCanvas;
    [SerializeField] GameObject _pauseButton;
    [SerializeField] private bool _isPaused;

    protected new void Start()
    {
        base.Start();
        _isPaused = false;
        Time.timeScale = 1f;
    }

    public void ResumeGame()
    {
        if(_isPaused)
        {
            _pauseButton.SetActive(true);
            _pauseMenuCanvas.enabled = false;

            _isPaused = false;
            Time.timeScale = 1f;
        }
    }

    public void PauseGame()
    {
        if(!_isPaused)
        {
            _pauseButton.SetActive(false);
            _isPaused = true;
            Time.timeScale = 0f;
            _pauseMenuCanvas.enabled = true;
        }
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
