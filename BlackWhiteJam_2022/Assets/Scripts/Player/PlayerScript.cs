using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private PlayerMovement _movementScript;
    public PlayerMovement MovementScript{ get{return _movementScript;} }
    [SerializeField] private PlayerInputHandler _inputHandler;
    [SerializeField] private PlayerCollisionHandler _collisionHandler;
    public PlayerCollisionHandler CollisionHandler { get {return _collisionHandler;}}
    [SerializeField] private PlayerSwipe _swipeHandler;
    public PlayerSwipe SwipeHandler { get {return _swipeHandler;} }
    [SerializeField] private PlayerLiquify _liquifyHandler;
    public PlayerLiquify LiquifyHandler { get {return _liquifyHandler;} }

    [SerializeField] private PlayerAnims _animHandler;
    public PlayerAnims AnimHandler { get {return _animHandler;}}

    [Header("Component References")]
    [SerializeField] private Rigidbody2D _rigidBody;
    public Rigidbody2D RB2D{ get{return _rigidBody;} }

    //Movement Ref
    private Vector2 _direction;
    public Vector2 Direction{ get{return _direction;} set{_direction = value;}}
    private Vector2 _isFacing = Vector2.right;
    public Vector2 IsFacing{ get{return _isFacing;} set{_isFacing = value;}}
    
    private bool _slipping;
    public bool OnSlipperySurface{ get {return _slipping;} set {_slipping = value;}}
    private bool _liquified;
    public bool Liquified{ get{return _liquified;} set {_liquified = value;}}

    private bool _isGrounded;
    public bool Grounded{ get{return _collisionHandler.IsGrounded();}}

    private bool _isJumpingUp;
    public bool IsJumpingUp{ get{return _isJumpingUp;} set {_isJumpingUp = value;}}
    private bool _isFallingDown;
    public bool IsFallingDown{ get{return _isFallingDown;} set {_isFallingDown = value;}}
    [SerializeField] private bool _isSwiping;
    public bool IsSwiping{ get{return _isSwiping;} set {_isSwiping = value;}}


    public bool CanJumpDown()
    {
        return true;
    }

    [SerializeField] private Animator _animator;
    public Animator Animator{ get {return _animator;} }

}
