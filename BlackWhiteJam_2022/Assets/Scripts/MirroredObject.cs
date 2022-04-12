using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirroredObject : MonoBehaviour
{
    [SerializeField] protected GameObject[] _mirrorObject;
    public GameObject[] MirrorObject {get{return _mirrorObject;}}
    private float[] _mirrorXDiff;
    static float _levelLoopDistance = 18;
    [SerializeField] private bool _randomizeSprite = false;
    [SerializeField] private SpriteRandomizer _spriteRandomizer;
    protected virtual void Start()
    {
        InitialSetup();
        AlignMirrors();
        
    }

    protected virtual void InitialSetup()
    {
        _mirrorXDiff = new float[_mirrorObject.Length];
        if(_mirrorObject != null)
        {
            for(int i = 0; i < _mirrorObject.Length; i++)
            {
                _mirrorXDiff[i] = _mirrorObject[i].transform.position.z * (_levelLoopDistance);
            }
        }

        if(_randomizeSprite && _spriteRandomizer != null)
        {
            this.GetComponent<SpriteRenderer>().sprite = _spriteRandomizer.GetRandomSprite();
        }
    }

    public virtual void AlignMirrors()
    {
        if(_mirrorObject != null)
        {
            for(int i = 0; i < _mirrorObject.Length; i++)
            {
                _mirrorObject[i].transform.position = new Vector3(transform.position.x +_mirrorXDiff[i], transform.position.y, transform.position.z);
                _mirrorObject[i].transform.rotation = transform.rotation;
            
                if(_randomizeSprite && _spriteRandomizer != null)
                {
                    _mirrorObject[i].GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;
                }
            }

        }
    }

}
