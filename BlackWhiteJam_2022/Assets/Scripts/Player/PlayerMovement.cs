using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerScript _playerScript;

    [Header("Basic Left/Right Movement")]
    [Tooltip("Force applied to allow for movement")] [Range(5f,60f)] [SerializeField] private float _movementForce = 30f;
    [Range(5f,60f)] [SerializeField] private float _maxVelocity = 30f;
    [Range(0f, 2f)] [SerializeField] private float _grip = 1f;
    [Range(0f, 1f)] [SerializeField] private float _decelerationMultiplier = 0.1f;
    
    [Header("Jump")]
    [Range(0.1f, 10f)] [SerializeField] private float _jumpHeight = 3f;
    [Range(0f, 1f)] [SerializeField] private float _gravity = 1f;
    [Range(0,3f)] [SerializeField] private float _fallGravityMultiplier = 2f;
    [Range(0f, 0.5f)] [SerializeField] private float _rigidBodyDrag = 0.15f;
    [Range(0f, 3f)] [SerializeField] private float _jumpVelocityCutoff = 1f;
    private float _globalGravity = -9.81f;
    private float _gravityScale = 0f;
    [SerializeField] private float _swipeTimer = 0f;

    public void Jump()
    {
        _playerScript.IsJumpingUp = true;
        _playerScript.RB2D.velocity = new Vector2(_playerScript.RB2D.velocity.x, 0f);
        _playerScript.RB2D.AddForce(Vector2.up * Mathf.Sqrt(_jumpHeight * -2f * Physics.gravity.y), ForceMode2D.Impulse);
        if(_playerScript.Liquified)
        {
            _playerScript.Liquified = false;
        }
    }

    private void FixedUpdate()
    {
        ApplyMovement();

        if(!_playerScript.Liquified)
        {
            if(_playerScript.IsSwiping)
            {
                _playerScript.AnimHandler.PlayAttackAnim();
            }
            else if(!_playerScript.Grounded && _playerScript.RB2D.velocity.y < 0)
            {
                if(_playerScript.IsJumpingUp)
                {
                    _playerScript.IsJumpingUp = false;
                }
                _playerScript.IsFallingDown = true;
                _playerScript.AnimHandler.PlayFallDown();
            }
            else if(_playerScript.IsJumpingUp)
            {
                _playerScript.AnimHandler.PlayJumpUp();
            }
            else if((_playerScript.IsFallingDown && _playerScript.Grounded) || (_playerScript.IsFallingDown &&  _playerScript.RB2D.velocity.y == 0) )
            {
                _playerScript.AnimHandler.PlayLand();
                _playerScript.IsFallingDown = false;
            }
            else if(_playerScript.Direction.magnitude <= 0.01f && _playerScript.RB2D.velocity.y == 0)
            {
                _playerScript.AnimHandler.PlayIdleAnim();
            }
            else if(_playerScript.Direction.magnitude >0.01f)
            {
                _playerScript.AnimHandler.PlayMoveAnim();
            }
        }

        if(_playerScript.IsSwiping)
        {
            Debug.Log(Time.deltaTime);
            _swipeTimer += Time.deltaTime;
            if(_swipeTimer >= 0.25f)
            {
                _playerScript.IsSwiping = false;
                _swipeTimer = 0f;
            }
        }

        Vector2 gravity = _globalGravity * _gravityScale * Vector2.up;
        _playerScript.RB2D.AddForce(gravity, ForceMode2D.Force);
    }

    //If there is input, players will move in the direction of the input.
    private void ApplyMovement()
    {
        if(_playerScript.Direction.magnitude >= 0.1f && !_playerScript.OnSlipperySurface)
        {            
            _playerScript.IsFacing = _playerScript.Direction;
            if(_playerScript.RB2D.velocity.magnitude >= _maxVelocity)
            {
                _playerScript.RB2D.velocity = Vector2.ClampMagnitude(_playerScript.RB2D.velocity, _maxVelocity);
            }
            else
            {
                _playerScript.RB2D.AddForce(_playerScript.Direction.normalized * _movementForce, ForceMode2D.Force);
            }

            //Impulses to compensate for sliding when turning
            float slideSpeed = Vector2.Dot(transform.right, _playerScript.RB2D.velocity);
            Vector2 impulse = transform.right * (-slideSpeed * _playerScript.RB2D.mass * ( _grip * 5));
            _playerScript.RB2D.AddForce(impulse);
            
        }
        else
        {
            if(!_playerScript.OnSlipperySurface)
            {
                //Decelerate player
                _playerScript.RB2D.velocity = new Vector2(_playerScript.RB2D.velocity.x * _decelerationMultiplier,
                                                    _playerScript.RB2D.velocity.y);
            }            
        }

        if(_playerScript.Grounded)
        {
            _gravityScale = 0;
            _rigidBodyDrag = 1;
        }
        else if (!_playerScript.OnSlipperySurface)
        {
            _gravityScale = _gravity;
            _playerScript.RB2D.drag = _rigidBodyDrag;
            if(_playerScript.RB2D.velocity.y < _jumpVelocityCutoff)
            {
                _gravityScale = _gravity * _fallGravityMultiplier;
            }
        }
    }
}
