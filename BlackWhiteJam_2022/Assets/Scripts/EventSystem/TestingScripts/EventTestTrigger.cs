using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTestTrigger : MonoBehaviour
{
    [SerializeField] private GameEvent _gameEvent;

    private void OnCollisionStay2D(Collision2D collisionInfo)
    {
        _gameEvent.Invoke();
    }

}
