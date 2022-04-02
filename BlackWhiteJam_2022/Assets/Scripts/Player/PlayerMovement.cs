using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;

    [Header("Player Input")]
    [SerializeField] private PlayerInput _playerInput;

    

    [Header("Basic Left/Right Movement")]
    [Tooltip("Force applied to allow for movement")] [Range(5f,40f)] [SerializeField] private float _movementForce = 30f;
    [Range(5f,40f)] [SerializeField] private float _maxVelocity = 30f;
    [Range(0f, 2f)] [SerializeField] private float _grip = 1f;
    [Range(0f, 1f)] [SerializeField] private float _decelerationMultiplier = 0.1f;
    private Vector2 _direction;
    
    private InputAction _moveAction;
    //private bool _liquified;

    /*[Header("Jump")]
    [SerializeField] private Transform _groundCheck;
    [Range(0f, 0.5f)][SerializeField] private float _groundDistance = 0.4f;
    [SerializeField] private LayerMask _groundMask;
    [Range(0.1f, 5f)] [SerializeField] private float _jumpHeight = 3f;
    [Range(0f, 1f)] [SerializeField] private float _gravity = 1f;
    [Range(0,3f)] [SerializeField] private float _fallGravityMultiplier = 2f;
    [Range(0f, 0.5f)] [SerializeField] private float _rigidBodyDrag = 0.15f;
    [Range(0f, 3f)] [SerializeField] private float _jumpVelocityCutoff = 1f;
    private float _globalGravity = -9.81f;
    private float _gravityScale = 0f;
    private bool _isGrounded;
    private InputAction _jumpAction;
    */

    private void Awake()
    {
        _moveAction = _playerInput.actions["Move"];
    }

    private void OnEnable()
    {
        _moveAction.started += OnMovement;
        _moveAction.performed += OnMovement;
        _moveAction.canceled += OnMovement;
    }

    private void OnDisable()
    {
        _moveAction.started += OnMovement;
        _moveAction.performed += OnMovement;
        _moveAction.canceled += OnMovement;
    }

    //When in free movement, context will have a float value
    //When in liquify movement, context will have a vector2 value
    private void OnMovement(InputAction.CallbackContext context)
    {
        float inputValue = context.ReadValue<float>();
        _direction = new Vector2(inputValue, 0f).normalized;
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    //If there is input, players will move in the direction of the input.
    private void ApplyMovement()
    {
        if(_direction.magnitude >= 0.1f)
        {            
            if(_rigidbody.velocity.magnitude >= _maxVelocity)
            {
                _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, _maxVelocity);
            }
            else
            {
                _rigidbody.AddForce(_direction.normalized * _movementForce, ForceMode2D.Force);
            }

            //Impulses to compensate for sliding when turning
            float slideSpeed = Vector2.Dot(transform.right, _rigidbody.velocity);
            Vector2 impulse = transform.right * (-slideSpeed * _rigidbody.mass * ( _grip * 5));
            _rigidbody.AddForce(impulse);
            
        }
        else
        {
            //Decelerate player
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x * _decelerationMultiplier, _rigidbody.velocity.y);
        }

        
        
    }
}
