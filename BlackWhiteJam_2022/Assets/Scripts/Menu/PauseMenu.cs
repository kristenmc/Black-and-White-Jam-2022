using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MainMenu
{
    [SerializeField] private Canvas _pauseMenuCanvas;
    [SerializeField] GameObject _pauseButton;
    [SerializeField] private bool _isPaused;
    [SerializeField] private TextMeshProUGUI _barkTMProUGUI;
    [SerializeField] private TextBarkHolder _textBarks;

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
            if(_textBarks != null && _barkTMProUGUI != null)
            {
                _barkTMProUGUI.text =_textBarks.GetRandomBarkText();
            }
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
