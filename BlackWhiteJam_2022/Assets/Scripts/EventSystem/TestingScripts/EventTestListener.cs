using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTestListener : GameEventListener
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public override void RaiseEvent()
    {
        base.RaiseEvent();
        _spriteRenderer.color = Color.blue;
    }
}
