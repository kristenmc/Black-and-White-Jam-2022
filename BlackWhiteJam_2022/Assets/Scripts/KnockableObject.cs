using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockableObject : MonoBehaviour
{
    [SerializeField] private GameEvent _knockdownGameEvent;
    [SerializeField] private GameObject _knockdownMirrorObject;
    private float _mirrorXDiff;
    [SerializeField] private Rigidbody2D _knockdownRigidBody;
    [SerializeField] private BoxCollider2D _knockdownCollider;
    [SerializeField] private float _knockdownLaunchForce;
    private bool _isKnocked = false;
    public bool IsKnocked{get{return _isKnocked;}}
    // Start is called before the first frame update
    void Start()
    {
        if(_knockdownMirrorObject != null)
        {
            _mirrorXDiff = _knockdownMirrorObject.transform.position.x - transform.position.x;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_knockdownMirrorObject != null && _isKnocked)
        {
            _knockdownMirrorObject.transform.position = new Vector3(transform.position.x +_mirrorXDiff, transform.position.y, transform.position.z);
            _knockdownMirrorObject.transform.rotation = transform.rotation;
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
