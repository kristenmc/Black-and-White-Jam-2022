using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserKnockableObject : KnockableObject
{
    [SerializeField] private GameObject _inactiveChaser;
    [SerializeField] private ChaserScript _chaserScript;
    
    [SerializeField] private string _cryAnim;
    public override void KnockDownObject()
    {
        base._knockdownGameEvent.Invoke();
        //Start the knockdown object animation here
        base.KnockDownObject();
        
    }

    public void ActivateChaser()
    {
        _inactiveChaser.SetActive(true);
        _chaserScript.IsActive = true;
        gameObject.transform.parent.gameObject.SetActive(false);
    }

    public void ActivateCryAnimation()
    {
        PlayAnimation(_cryAnim);
    }
}
