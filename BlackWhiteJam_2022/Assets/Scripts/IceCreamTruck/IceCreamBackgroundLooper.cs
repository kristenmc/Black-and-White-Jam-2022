using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreamBackgroundLooper : MonoBehaviour
{
    [SerializeField] private Transform _teleportToLocation;
    [SerializeField] private Transform _teleportFromLocation;
    [SerializeField] private float _maxLoopingSpeed;
    private float _currentLoopingSpeed = 0f;
    [SerializeField] private float _incrementLoopingSpeedBy; 
    private bool _isLooping = false;
    // Start is called before the first frame update
    void Start()
    {
        _maxLoopingSpeed /= 100;
        _incrementLoopingSpeedBy /= 100;
    }

    // Update is called once per frame
    void Update()
    {
        if(_isLooping)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2 (_teleportFromLocation.position.x, transform.position.y), _currentLoopingSpeed);
            if(_currentLoopingSpeed > _maxLoopingSpeed)
            {
                _currentLoopingSpeed = _maxLoopingSpeed;
            }
            else if(_currentLoopingSpeed < _maxLoopingSpeed)
            {
                _currentLoopingSpeed += _incrementLoopingSpeedBy;
            }
            
        }
        if(transform.position.x <= _teleportFromLocation.position.x)
        {
            transform.position = new Vector2(_teleportToLocation.transform.position.x, transform.position.y);
        }
    }

    public void BeginLooping()
    {
        _isLooping = true;
    }
}
