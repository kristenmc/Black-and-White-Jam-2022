using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Text Bark Holder")]
public class TextBarkHolder : ScriptableObject
{
    [SerializeField] private TextBark[] _textBarks;
    private List<int> _randomIndices;
    private int _counter;

    private void OnEnable()
    {
        if(_randomIndices!= null)
        {
            _randomIndices.Clear();
        }
        _counter = 0;
        PopulateRandomIndices();
    }
    private void PopulateRandomIndices()
    {
        _randomIndices = new List<int>();
        for(int i = 0; i < _textBarks.Length; i++)
        {
            _randomIndices.Add(i);
        }
        ShuffleRandomIndices();
    }

    private void ShuffleRandomIndices()
    {
        for(int i = 0; i < _randomIndices.Count; i++)
        {
            int rnd = Random.Range(0, _randomIndices.Count);
            int temp = _randomIndices[rnd];
            _randomIndices[rnd] = _randomIndices[i];
            _randomIndices[i] = temp;
        }

    }
    public string GetRandomBarkText()
    {
        _counter++;
        if(_textBarks != null)
        {
            if(_counter == _randomIndices.Count)
            {
                _counter = 0;
                PopulateRandomIndices();
            }
            return _textBarks[_randomIndices[_counter]].Text;
        }
        return "";
        
    }
}
