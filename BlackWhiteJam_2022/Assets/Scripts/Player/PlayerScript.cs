using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private PlayerMovement _movementScript;
    public PlayerMovement MovementScript{ get{return _movementScript;} }
    [SerializeField] private PlayerInputHandler _inputHandler;
    [SerializeField] private PlayerCollisionHandler _collisionHandler;
    [SerializeField] private PlayerSwipe _swipeHandler;
    public PlayerSwipe SwipeHandler { get {return _swipeHandler;} }


    [Header("Component References")]
    [SerializeField] private Rigidbody2D _rigidBody;
    public Rigidbody2D RB2D{ get{return _rigidBody;} }

    //Movement Ref
    private Vector2 _direction;
    public Vector2 Direction{ get{return _direction;} set{_direction = value;}}
    
    private bool _liquified;
    public bool Liquified{ get{return _liquified;}}

    private bool _isGrounded;
    public bool Grounded{ get{return _isGrounded;} set{_isGrounded = value;}}

}
