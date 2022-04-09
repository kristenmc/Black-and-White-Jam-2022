using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToolTipHandler : MonoBehaviour
{
    [SerializeField] private ToolTip[] _toolTips;
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _TMProUGUI;

    [SerializeField] private GameObject _nextButton;
    [SerializeField] private GameObject _previousButton;
    private int _index = 0;

    private void OnEnable()
    {   
        StartToolTip(_index);
    }

    private void UpdateButtons()
    {
        _previousButton.SetActive(_toolTips[_index].CanGoBack());
        _nextButton.SetActive(_toolTips[_index].CanContinue());
    }
    
    private void SetUpToolTipSingle(ToolTipSingle singleTip)
    {
        //_image.sprite = singleTip.Sprite;
        _TMProUGUI.text = singleTip.Text;
        UpdateButtons();
    }
    public void StartToolTip(int i)
    {
        _index = i;
        if(_index < _toolTips.Length)
        {
            SetUpToolTipSingle(_toolTips[_index].Start());            
        }
    }

    public void Continue()
    {
        if(_index < _toolTips.Length && _nextButton.activeSelf != false)
        {
            SetUpToolTipSingle(_toolTips[_index].Next());            
        }
    }

    public void GoBack()
    {
        if(_index < _toolTips.Length && _previousButton.activeSelf != false)
        {
            SetUpToolTipSingle(_toolTips[_index].GoBack());            
        }
    }
}
