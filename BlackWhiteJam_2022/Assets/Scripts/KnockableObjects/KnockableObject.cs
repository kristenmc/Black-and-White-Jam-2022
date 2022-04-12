using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockableObject : MirroredObject
{
    [SerializeField] protected GameEvent _knockdownGameEvent;
    [SerializeField] protected Rigidbody2D _knockdownRigidBody;
    [SerializeField] protected Collider2D _knockdownCollider;
    [SerializeField] protected float _knockdownLaunchForce;
    [SerializeField] protected bool _canBeKnocked = true;
    public bool CanBeKnocked{get{return _canBeKnocked;} set{_canBeKnocked = value;}}
    private bool _countsForProgress = true;
    public bool CountsForProgress{get{return _countsForProgress;} set{_countsForProgress = value;}}
    [SerializeField] protected bool _randomRotation = false;
    public bool RandomRotation{get {return _randomRotation;}}
    [SerializeField] protected bool _ragDollPhysics = true;
    [SerializeField] protected string _fallSFXName = "Object_Fall";
    
    
    [Header("Animation")]
    [SerializeField] protected bool _hasAnimation = false;

    [SerializeField] private ChaserKnockableObject _chaser;
    [SerializeField] protected Animator _animator;
    [SerializeField] protected string _knockAnim;    
    
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

    protected void PlayAnimation(string animName)
    {
        _animator.Play(animName);
        if(_mirrorObject != null)
        {
            for(int i = 0; i < _mirrorObject.Length; i++)
            {
                _mirrorObject[i].GetComponent<Animator>().Play(animName);
            }

        }
    }
    private void PlayKnockAnimations()
    {
        PlayAnimation(_knockAnim);
        
    }

    
    public virtual void KnockDownObject()
    {
        if(_countsForProgress)
        {
            if(_hasAnimation)
            {
                if(_animator != null)
                {
                    PlayKnockAnimations();
                }
                else if (_chaser!= null)
                {
                    _chaser.ActivateCryAnimation();
                }
                
            }
            _knockdownGameEvent.Invoke();
            _countsForProgress = false;
        }
        //Start the knockdown object animation here
        if(_ragDollPhysics)
        {
            _knockdownRigidBody.constraints = RigidbodyConstraints2D.None;
            _knockdownRigidBody.AddForce(transform.up * _knockdownLaunchForce);
            _knockdownRigidBody.AddTorque(180);
        }
        
        _knockdownCollider.isTrigger = true;        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!_countsForProgress && other.gameObject.layer == 6)
        {
            //Replace with break animation later
            //Destroy(gameObject);
            _knockdownRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;

            FMODUnity.RuntimeManager.PlayOneShot("event:/KnockableObjects/" + _fallSFXName); //Play sound for when an objects hits the ground
        }    
    }
}
