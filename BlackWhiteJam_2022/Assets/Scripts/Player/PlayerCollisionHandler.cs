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
    /*private void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if(LayerMask.LayerToName(collisionInfo.gameObject.layer) == _jumpDownMask)
        {
            // YEs
        }
    }*/

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
