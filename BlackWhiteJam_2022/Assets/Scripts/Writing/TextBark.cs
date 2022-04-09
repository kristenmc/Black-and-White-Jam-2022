using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Text Bark")]
public class TextBark : ScriptableObject
{
    [SerializeField] private string _text;
    public string Text{ get {return _text;}}
}
