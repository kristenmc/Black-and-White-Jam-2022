using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockableObject : MirroredObject
{
    [SerializeField] private GameEvent _knockdownGameEvent;
    [SerializeField] private Rigidbody2D _knockdownRigidBody;
    [SerializeField] private BoxCollider2D _knockdownCollider;
    [SerializeField] private float _knockdownLaunchForce;
    private bool _isKnocked = true;
    public bool IsKnocked{get{return _isKnocked;}}
    [SerializeField] bool _randomRotation = false;
    [SerializeField] bool _ragDollPhysics = true;
    [SerializeField] string _fallSFXName = "Object_Fall";
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _isKnocked = false;
        
    }

    protected override void InitialSetup()
    {
        base.InitialSetup();
        if(_randomRotation)
        {
            Quaternion rotation = transform.localRotation;
            rotation.eulerAngles = new Vector3(rotation.eulerAngles.x, rotation.eulerAngles.y, Random.Range(-30, 30));
            transform.localRotation = rotation;
        }

    }

    void Update()
    {
        AlignMirrors();
    }

    public override void AlignMirrors()
    {
        if(_isKnocked)
        {
            base.AlignMirrors();
        }
        
    }

    
    public void KnockDownObject()
    {
        _knockdownGameEvent.Invoke();
        //Start the knockdown object animation here
        if(_ragDollPhysics)
        {
            _knockdownRigidBody.constraints = RigidbodyConstraints2D.None;
            _knockdownRigidBody.AddForce(transform.up * _knockdownLaunchForce);
            _knockdownRigidBody.AddTorque(180);
        }
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

            FMODUnity.RuntimeManager.PlayOneShot("event:/KnockableObjects/" + _fallSFXName); //Play sound for when an objects hits the ground
        }    
    }
}
