using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ToolTip/SingleTip")]
public class ToolTipSingle : ScriptableObject
{
    [TextArea(3,10)]
    [SerializeField] private string _text;
    public string Text{ get {return _text;}}
    [SerializeField] private Sprite _image;
    public Sprite Sprite{ get{return _image;}}   
}
