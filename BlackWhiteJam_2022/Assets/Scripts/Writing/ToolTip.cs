using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ToolTip/Holder")]
public class ToolTip : ScriptableObject
{
    [SerializeField] private ToolTipSingle[] _tips;
    private int _index;

    private void OnEnable()
    {
        _index = 0;
    }

    public bool CanContinue()
    {
        return _index < _tips.Length - 1;
    }

    public bool CanGoBack()
    {
        return _index > 0;
    }

    public ToolTipSingle Start()
    {
        _index = 0;
        return _tips[0];
    }
    public ToolTipSingle Next()
    {
        _index++;
        return _tips[_index];
    }

    public ToolTipSingle GoBack()
    {
        _index--;
        return _tips[_index];
    }

}
