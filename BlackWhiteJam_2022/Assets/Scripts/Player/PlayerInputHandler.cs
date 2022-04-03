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

    private void test()
    {
        _playerScript.RB2D.AddForce(Vector2.up);
    }

    private void Awake()
    {
        _moveAction = _playerInput.actions["Move"];
        _jumpAction = _playerInput.actions["Jump"];
        _swipeAction = _playerInput.actions["Swipe"];
    }

        private void OnEnable()
    {
        _moveAction.started += OnMovement;
        _moveAction.performed += OnMovement;
        _moveAction.canceled += OnMovement;

        _jumpAction.started += OnJump;
        _swipeAction.started += OnSwipe;
    }

    private void OnDisable()
    {
        _moveAction.started -= OnMovement;
        _moveAction.performed -= OnMovement;
        _moveAction.canceled -= OnMovement;

        _jumpAction.started -= OnJump;
        _swipeAction.started -= OnSwipe;
    }

    private void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 inputValue = context.ReadValue<Vector2>();
        if(!_playerScript.Liquified)
        {
            _playerScript.Direction = new Vector2(inputValue.x, 0f).normalized;
        }
        
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if(_playerScript.Grounded)
        {
            _playerScript.MovementScript.Jump();
        }
    }

    private void OnSwipe(InputAction.CallbackContext context)
    {
        _playerScript.SwipeHandler.Swipe();
    }
    
}
