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
    [Tooltip("Force applied to allow for movement")] [Range(5f,60f)] [SerializeField] private float _movementForce = 30f;
    [Range(5f,60f)] [SerializeField] private float _maxVelocity = 30f;
    [Range(0f, 2f)] [SerializeField] private float _grip = 1f;
    [Range(0f, 1f)] [SerializeField] private float _decelerationMultiplier = 0.1f;
    private Vector2 _direction;
    
    private InputAction _moveAction;
    private bool _liquified;

    [SerializeField] private bool _circleGroundCheck;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Transform _groundCheckPointA;
    [SerializeField] private Transform _groundCheckPointB;
    [Range(0f, 0.5f)][SerializeField] private float _groundDistance = 0.4f;
    [SerializeField] private LayerMask _groundMask;
    [Range(0.1f, 10f)] [SerializeField] private float _jumpHeight = 3f;
    [Range(0f, 1f)] [SerializeField] private float _gravity = 1f;
    [Range(0,3f)] [SerializeField] private float _fallGravityMultiplier = 2f;
    [Range(0f, 0.5f)] [SerializeField] private float _rigidBodyDrag = 0.15f;
    [Range(0f, 3f)] [SerializeField] private float _jumpVelocityCutoff = 1f;
    private float _globalGravity = -9.81f;
    private float _gravityScale = 0f;
    private bool _isGrounded;
    private InputAction _jumpAction;
    

    private void Awake()
    {
        _moveAction = _playerInput.actions["Move"];
        _jumpAction = _playerInput.actions["Jump"];
    }

    private void OnEnable()
    {
        _moveAction.started += OnMovement;
        _moveAction.performed += OnMovement;
        _moveAction.canceled += OnMovement;

        _jumpAction.started += OnJump;
    }

    private void OnDisable()
    {
        _moveAction.started += OnMovement;
        _moveAction.performed += OnMovement;
        _moveAction.canceled += OnMovement;

        _jumpAction.started += OnJump;
    }

    //When in free movement, context will have a float value
    //When in liquify movement, context will have a vector2 value
    private void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 inputValue = context.ReadValue<Vector2>();
        if(!_liquified)
        {
            _direction = new Vector2(inputValue.x, 0f).normalized;
        }
        
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if(_isGrounded)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0f);
            _rigidbody.AddForce(Vector2.up * Mathf.Sqrt(_jumpHeight * -2f * Physics.gravity.y), ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        if(_circleGroundCheck)
        {
            _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundDistance, _groundMask);
        }
        else
        {
            _isGrounded = Physics2D.OverlapArea(_groundCheckPointA.position, _groundCheckPointB.position, _groundMask);
        }
        


        ApplyMovement();

        Vector2 gravity = _globalGravity * _gravityScale * Vector2.up;
        _rigidbody.AddForce(gravity, ForceMode2D.Force);
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

        if(_isGrounded)
        {
            _gravityScale = 0;
            _rigidBodyDrag = 1;
        }
        else
        {
            _gravityScale = _gravity;
            _rigidbody.drag = _rigidBodyDrag;
            if(_rigidbody.velocity.y < _jumpVelocityCutoff)
            {
                _gravityScale = _gravity * _fallGravityMultiplier;
            }

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
