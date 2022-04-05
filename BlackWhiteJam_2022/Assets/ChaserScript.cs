using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserScript : MonoBehaviour
{
    [SerializeField] Transform _leftZone;
    [SerializeField] Transform _rightZone;
    [SerializeField] float _movementForce;
    [SerializeField] float _patrolVelocity;
    [SerializeField] float _chaseVelocity;
    private Rigidbody2D _rb;
    private bool _facingRight;
    private bool _isActive;
    public bool IsActive{ get{return _isActive;} set{_isActive = value;}}
    private bool _isChasing;

    // Start is called before the first frame update
    void Start()
    {
        _isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() 
    {
        if(_isActive)
        {
            PatrolForPlayer();
        }
    }

    public void PatrolForPlayer()
    {
        /*
        if((!_isChasing && _rb.velocity.magnitude >= _patrolVelocity) || (_isChasing && _rb.velocity.magnitude >= _chaseVelocity))
        {
            _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, _isChasing ? _chaseVelocity :_patrolVelocity);
        }
        else
        {
            _rb.AddForce(_facingRight ? Vector2.right : Vector2.left * _movementForce, ForceMode2D.Force);
        }
        */
        if(!_isChasing)
        {
            if(_facingRight)
            {
                Debug.Log("mvoign right");
                transform.position = Vector2.MoveTowards(transform.position, new Vector2 (_rightZone.position.x, transform.position.y), _patrolVelocity);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2 (_leftZone.position.x, transform.position.y), _patrolVelocity);
            }
        }
        if(transform.position.x >= _rightZone.position.x)
        {
            _facingRight = false;
        }
        else if(transform.position.x <= _leftZone.position.x)
        {
            _facingRight = true;
        }
    }
}
