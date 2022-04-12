using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserKnockableObject : KnockableObject
{
    [SerializeField] private GameObject _inactiveChaser;
    [SerializeField] private ChaserScript _chaserScript;
    
    public override void KnockDownObject()
    {
        base._knockdownGameEvent.Invoke();
        //Start the knockdown object animation here
        base.KnockDownObject();
        
    }

    //#TODO call this function at the end of the Knockover animation using an animation event
    public void ActivateChaser()
    {
        _inactiveChaser.SetActive(true);
        _chaserScript.IsActive = true;
        //#TODO turn off the sprite for the knockable version of the kid
    }
}
