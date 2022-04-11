using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockableObject : MirroredObject
{
    [SerializeField] private GameEvent _knockdownGameEvent;
    [SerializeField] private Rigidbody2D _knockdownRigidBody;
    [SerializeField] private Collider2D _knockdownCollider;
    [SerializeField] private float _knockdownLaunchForce;
    [SerializeField] private bool _canBeKnocked = true;
    public bool CanBeKnocked{get{return _canBeKnocked;} set{_canBeKnocked = value;}}
    [SerializeField] private bool _randomRotation = false;
    public bool RandomRotation{get {return _randomRotation;}}
    [SerializeField] private bool _ragDollPhysics = true;
    [SerializeField] private string _fallSFXName = "Object_Fall";
    [SerializeField] private bool _hasAnimation = false;
    [SerializeField] private Animation _knockAnimation; 
    
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();        
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
        base.AlignMirrors();
        
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
        if(_hasAnimation)
        {
            //Uh make this actually work i think
            _knockAnimation.Play();
        }
        _canBeKnocked = false;
        _knockdownCollider.isTrigger = true;
        //Then probably destroy or hide the object
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!_canBeKnocked && other.gameObject.layer == 6)
        {
            //Replace with break animation later
            //Destroy(gameObject);
            _knockdownRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;

            FMODUnity.RuntimeManager.PlayOneShot("event:/KnockableObjects/" + _fallSFXName); //Play sound for when an objects hits the ground
        }    
    }
}
