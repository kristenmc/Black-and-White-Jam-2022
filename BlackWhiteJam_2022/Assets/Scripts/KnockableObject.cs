using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockableObject : MonoBehaviour
{
    [SerializeField] private GameEvent _knockdownGameEvent;
    [SerializeField] private GameObject[] _knockdownMirrorObject;
    private float[] _mirrorXDiff;
    [SerializeField] private Rigidbody2D _knockdownRigidBody;
    [SerializeField] private BoxCollider2D _knockdownCollider;
    [SerializeField] private float _knockdownLaunchForce;
    private bool _isKnocked = true;
    public bool IsKnocked{get{return _isKnocked;}}
    [SerializeField] bool _randomRotation = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _mirrorXDiff = new float[_knockdownMirrorObject.Length];
        if(_knockdownMirrorObject != null)
        {
            for(int i = 0; i < _knockdownMirrorObject.Length; i++)
            {
                _mirrorXDiff[i] = _knockdownMirrorObject[i].transform.position.x - transform.position.x;
            }
        }
        if(_randomRotation)
        {
            Quaternion rotation = transform.localRotation;
            rotation.eulerAngles = new Vector3(rotation.eulerAngles.x, rotation.eulerAngles.y, Random.Range(-30, 30));
            transform.localRotation = rotation;
        }
        AlignMirrors();
        _isKnocked = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        AlignMirrors();
    }

    public void AlignMirrors()
    {
        if(_knockdownMirrorObject != null && _isKnocked)
        {
            for(int i = 0; i < _knockdownMirrorObject.Length; i++)
            {
                _knockdownMirrorObject[i].transform.position = new Vector3(transform.position.x +_mirrorXDiff[i], transform.position.y, transform.position.z);
                _knockdownMirrorObject[i].transform.rotation = transform.rotation;
            }

        }
    }
    
    public void KnockDownObject()
    {
        _knockdownGameEvent.Invoke();
        //Start the knockdown object animation here
        _knockdownRigidBody.constraints = RigidbodyConstraints2D.None;
        _knockdownRigidBody.AddForce(transform.up * _knockdownLaunchForce);
        _knockdownRigidBody.AddTorque(180);
        _isKnocked = true;
        _knockdownCollider.isTrigger = true;
        //Then probably destroy or hide the object
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(_isKnocked && other.gameObject.layer == 6)
        {
            //Replace with break animation later
            //Destroy(gameObject);
            _knockdownRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        }    
    }
}
