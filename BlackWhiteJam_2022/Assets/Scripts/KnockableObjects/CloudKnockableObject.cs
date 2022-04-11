using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudKnockableObject : KnockableObject
{
    [SerializeField] private float _offsetDistance;
    private float[] _cloudMirrorXDiff;

    protected override void InitialSetup()
    {
        _cloudMirrorXDiff = new float[base.MirrorObject.Length];
        if(base.MirrorObject != null)
        {
            for(int i = 0; i < base.MirrorObject.Length; i++)
            {
                _cloudMirrorXDiff[i] = base.MirrorObject[i].transform.position.z * (_offsetDistance);
            }
        }
        if(base.RandomRotation)
        {
            Quaternion rotation = transform.localRotation;
            rotation.eulerAngles = new Vector3(rotation.eulerAngles.x, rotation.eulerAngles.y, Random.Range(-10, 10));
            transform.localRotation = rotation;
        }
    }

    public override void AlignMirrors()
    {
        if(!base.CanBeKnocked)
        {
            if(base.MirrorObject != null)
            {
                for(int i = 0; i < base.MirrorObject.Length; i++)
                {
                    base.MirrorObject[i].transform.position = new Vector3(transform.position.x +_cloudMirrorXDiff[i], transform.position.y, transform.position.z);
                    base.MirrorObject[i].transform.rotation = transform.rotation;
                }

            }
        }
        
    }
}
