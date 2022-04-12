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
    [SerializeField] float _chaseDistance;
    private Rigidbody2D _rb;
    private bool _facingRight;
    private bool _isActive;
    public bool IsActive{ get{return _isActive;} set{_isActive = value;}}
    private bool _isChasing;
    public bool IsChasing{ get{return _isChasing;} set{_isChasing = value;}}
    private GameObject _player;
    private PlayerScript _playerScript;
    private Vector2 _originalPosition;
    [SerializeField] private GameEvent _loseGame;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;
    [SerializeField] private string _walkAnim;
    [SerializeField] private string _runAnim;

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
                transform.position = Vector2.MoveTowards(transform.position, new Vector2 (_rightZone.position.x, transform.position.y), _patrolVelocity);
                _spriteRenderer.flipX = true;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2 (_leftZone.position.x, transform.position.y), _patrolVelocity);
                _spriteRenderer.flipX = false;
            }
        }
        else if(_player != null && !_playerScript.Liquified)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2 (_player.transform.position.x, transform.position.y), _chaseVelocity);
            _animator.Play(_runAnim);
            if(Mathf.Abs(transform.position.x - _originalPosition.x) >= _chaseDistance)
            {
                _isChasing = false;
                _animator.Play(_walkAnim);
            }
        }
        else if(_playerScript.Liquified)
        {
            _isChasing = false;
            _animator.Play(_walkAnim);
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

    public void DetectPlayer(GameObject player)
    {
        _isChasing = true;
        _player = player;
        _playerScript = player.GetComponent<PlayerScript>();
        _originalPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        //Game over or something idk 
        _loseGame.Invoke();
    }
}
