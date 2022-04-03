using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockableObject : MonoBehaviour
{
    [SerializeField] private GameEvent _knockdownGameEvent;
    [SerializeField] private Rigidbody2D _knockdownRigidBody;
    [SerializeField] private BoxCollider2D _knockdownCollider;
    [SerializeField] private float _knockdownLaunchForce;
    private bool _isKnocked = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KnockDownObject()
    {
        _knockdownGameEvent.Invoke();
        //Start the knockdown object animation here
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
            Destroy(gameObject);
        }    
    }
}
