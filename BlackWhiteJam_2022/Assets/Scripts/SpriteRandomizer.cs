using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sprite Randomizer")]
public class SpriteRandomizer : ScriptableObject
{
    [SerializeField] private Sprite[] _sprites;

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
        for(int i = 0; i < _sprites.Length; i++)
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
    public Sprite GetRandomSprite()
    {
        _counter++;
        if(_sprites != null)
        {
            if(_counter == _randomIndices.Count)
            {
                _counter = 0;
                PopulateRandomIndices();
            }
            return _sprites[_randomIndices[_counter]];
        }
        return null;
        
    }
}
