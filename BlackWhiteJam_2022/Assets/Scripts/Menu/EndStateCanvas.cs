using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;

public class EndStateCanvas : MainMenu
{
    [SerializeField] private Canvas _gameFailCanvas;
    [SerializeField] private Canvas _gameWinCanvas;

    [SerializeField] private TextMeshProUGUI _barkTMProUGUIFail;
    [SerializeField] private TextBarkHolder _textBarksFail;
    [SerializeField] private TextMeshProUGUI _barkTMProUGUIWin;
    [SerializeField] private TextBarkHolder _textBarksWin;

    public void LoseGame()
    {
        _gameFailCanvas.enabled = true;
        if(_textBarksFail != null && _barkTMProUGUIFail != null)
        {
            _barkTMProUGUIFail.text =_textBarksFail.GetRandomBarkText();
        }

        Time.timeScale = 0f;
    }

    public void WinGame()
    {
        if(_textBarksWin != null && _barkTMProUGUIWin != null)
        {
            _barkTMProUGUIWin.text =_textBarksWin.GetRandomBarkText();
        }
        _gameWinCanvas.enabled = true;
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
