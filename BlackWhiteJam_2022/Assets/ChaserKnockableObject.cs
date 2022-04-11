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
        if(_ragDollPhysics)
        {
            _knockdownRigidBody.constraints = RigidbodyConstraints2D.None;
            _knockdownRigidBody.AddForce(transform.up * _knockdownLaunchForce);
            _knockdownRigidBody.AddTorque(180);
        }
        if(_hasAnimation)
        {
            //#TODO implement animation start code
        }
        _canBeKnocked = false;
        _knockdownCollider.isTrigger = true;
        //Then probably destroy or hide the object
        
    }

    //#TODO call this function at the end of the Knockover animation using an animation event
    public void ActivateChaser()
    {
        _inactiveChaser.SetActive(true);
        _chaserScript.IsActive = true;
        //#TODO turn off the sprite for the knockable version of the kid
    }
}
