using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MainMenu
{
    [SerializeField] private Canvas _pauseMenuCanvas;
    [SerializeField] GameObject _pauseButton;
    [SerializeField] private BoolVariable _isPaused;
    [SerializeField] private TextMeshProUGUI _barkTMProUGUI;
    [SerializeField] private TextBarkHolder _textBarks;

    protected new void Start()
    {
        base.Start();
        _isPaused.Value = false;
        
    }

    public void ResumeGame()
    {
        if(_isPaused.Value)
        {
            _pauseButton.SetActive(true);
            _pauseMenuCanvas.enabled = false;

            _isPaused.Value = false;
            Time.timeScale = 1f;

            FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Unpause"); //Play pause sound
        }
    }

    public void PauseGame()
    {
        if(!_isPaused.Value)
        {
            if(_textBarks != null && _barkTMProUGUI != null)
            {
                _barkTMProUGUI.text =_textBarks.GetRandomBarkText();
            }
            _pauseButton.SetActive(false);
            _isPaused.Value = true;
            Time.timeScale = 0f;
            _pauseMenuCanvas.enabled = true;

            FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Pause"); //Play unpause sound
        }
    }

    public void ResetLevel()
    {
        AudioManager.Instance.ResetMusic();
        LoadScene(SceneManager.GetActiveScene().name);

    }

    public void MainMenu()
    {
        AudioManager.Instance.ResetMusic();
        LoadScene("MainMenu");
    }

}
