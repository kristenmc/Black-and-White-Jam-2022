using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private PlayerScript _playerScript;
    [SerializeField] private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _swipeAction;
    private InputAction _liquifyAction;
    private bool _pressDown;
    [SerializeField] private BoolVariable _isPaused;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    

    private void test()
    {
        _playerScript.RB2D.AddForce(Vector2.up);
    }

    private void Awake()
    {
        _moveAction = _playerInput.actions["Move"];
        _jumpAction = _playerInput.actions["Jump"];
        _swipeAction = _playerInput.actions["Swipe"];
        _liquifyAction = _playerInput.actions["Liquify"];
    }

        private void OnEnable()
    {
        _moveAction.started += OnMovement;
        _moveAction.performed += OnMovement;
        _moveAction.canceled += OnMovement;

        _jumpAction.started += OnJump;
        _swipeAction.started += OnSwipe;
        _liquifyAction.started += OnLiquify;
    }

    private void OnDisable()
    {
        _moveAction.started -= OnMovement;
        _moveAction.performed -= OnMovement;
        _moveAction.canceled -= OnMovement;

        _jumpAction.started -= OnJump;
        _swipeAction.started -= OnSwipe;
        _liquifyAction.started -= OnLiquify;
    }

    private void OnMovement(InputAction.CallbackContext context)
    {
        if(!_isPaused.Value && !_playerScript.Liquified)
        {
            _playerScript.AnimHandler.PlayMoveAnim();
            Vector2 inputValue = context.ReadValue<Vector2>();
            _playerScript.Direction = new Vector2(inputValue.x, 0f).normalized;

            _pressDown = inputValue.y <= -0.1f ? true : false;
            if(_playerScript.Direction.x == 1)
            {
                _spriteRenderer.flipX = false;
            }
            else if (_playerScript.Direction.x == -1)
            {
                _spriteRenderer.flipX = true;
            }

            if(_playerScript.Direction.magnitude <= 0.01f)
            {
                _playerScript.AnimHandler.PlayIdleAnim();
            }
        }
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if(!_isPaused.Value)
        {
            _playerScript.AnimHandler.PlayJump();
            if(_pressDown && _playerScript.CollisionHandler.OnPlatform())
            {
                _playerScript.CollisionHandler.JumpDown();
            }
            else if(_playerScript.Grounded)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Jump"); //Play jump sound
                _playerScript.MovementScript.Jump();
            }
        }
        
    }

    private void OnSwipe(InputAction.CallbackContext context)
    {
        if(!_isPaused.Value)
        {
            _playerScript.SwipeHandler.Swipe();
        }
        
    }

    private void OnLiquify(InputAction.CallbackContext context)
    {
        if(!_isPaused.Value)
        {
            _playerScript.LiquifyHandler.Liquify();
            _playerScript.Direction = Vector2.zero;
        }
        
    }
    
}
