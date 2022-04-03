using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockableObject : MonoBehaviour
{
    [SerializeField] private GameEvent _knockdownGameEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KnockDownObject()
    {
        _knockdownGameEvent.Invoke();
        //Start the knockdown object animation here
        //Then probably destroy or hide the object
    }
}
