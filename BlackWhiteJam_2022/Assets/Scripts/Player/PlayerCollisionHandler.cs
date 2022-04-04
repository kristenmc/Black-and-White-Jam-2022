using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private PlayerScript _playerScript;
    
    [Header("Grounded Check")]
    [SerializeField] private bool _circleGroundCheck;
    [SerializeField] private Transform _groundCheck;
    [Range(0f, 0.5f)][SerializeField] private float _groundDistance = 0.1f;
    [SerializeField] private Transform _groundCheckPointA;
    [SerializeField] private Transform _groundCheckPointB;
    [SerializeField] private LayerMask _groundMask;

    [Header("Jump Down Var")]
    [SerializeField] private float _jumpDownCheckDistance = 1f;
    [SerializeField] private LayerMask _jumpDownMask;
    [SerializeField] private BoxCollider2D _collider;
    private Collider2D _lastPlatform = null;

    /*[Header("Jump Down from Platform Check")]
    [SerializeField] private string _jumpDownMask;
    private Collision2D _lastPlatform = null;
    private bool _pressDown = false;*/

    private void FixedUpdate()
    {
        if(_circleGroundCheck)
        {
            _playerScript.Grounded = Physics2D.OverlapCircle(_groundCheck.position, _groundDistance, _groundMask);
        }
        else
        {
            _playerScript.Grounded = Physics2D.OverlapArea(_groundCheckPointA.position, _groundCheckPointB.position, _groundMask);
        }
    }

    public bool OnPlatform()
    {
        bool onPlatform;
        if(_circleGroundCheck)
        {
            onPlatform = Physics2D.OverlapCircle(_groundCheck.position, _groundDistance, _jumpDownMask);
        }
        else
        {
            onPlatform = Physics2D.OverlapArea(_groundCheckPointA.position, _groundCheckPointB.position, _jumpDownMask);
        }
        return true;
    }

    public void JumpDown()
    {
        RaycastHit2D rayHit;
        if(_circleGroundCheck)
        {
            rayHit = Physics2D.CircleCast(_groundCheck.position,_groundDistance, Vector2.down, _jumpDownMask);
        }
        else
        {
            rayHit = Physics2D.BoxCast((_groundCheckPointA.position + _groundCheckPointB.position)/2f,
                                        new Vector2(Mathf.Abs(_groundCheckPointA.position.x - _groundCheckPointB.position.x),
                                                    Mathf.Abs(_groundCheckPointA.position.y - _groundCheckPointB.position.y)),
                                        0f,Vector2.down, _jumpDownCheckDistance, _jumpDownMask);
        }

        if(rayHit.collider!=null && _collider!=null)
        {
            _lastPlatform = rayHit.collider;
            Physics2D.IgnoreCollision(_collider, _lastPlatform, true);
        }
    }
    private void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if(_playerScript.Grounded && _lastPlatform != null)
        {
            Physics2D.IgnoreCollision(_collider, _lastPlatform, false);
            _lastPlatform = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(_circleGroundCheck)
        {
            Gizmos.DrawWireSphere(_groundCheck.position, _groundDistance);
        }
        else
        {
            Gizmos.DrawLine(_groundCheckPointA.position, _groundCheckPointB.position);
        }
    }
}
