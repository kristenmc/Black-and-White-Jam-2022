using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ObjectiveHolder")]
public class ObjectiveHolder : ScriptableObject
{
    [SerializeField] private TextBark[] _textBarks;
    private int _counter = 0;
    private void OnEnable()
    {
        _counter = 0;
    }

    public string GetCurrentTextBark()
    {
        if (_counter==_textBarks.Length)
        {
            return "";
        }
        else
        {
            _counter++;
            return _textBarks[_counter-1].Text;
        }
    }
}
