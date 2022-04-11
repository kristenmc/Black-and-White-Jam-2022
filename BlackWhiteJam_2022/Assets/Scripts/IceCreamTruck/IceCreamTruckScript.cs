using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreamTruckScript : MonoBehaviour
{
    [SerializeField] private Transform _teleportToLocation;
    public Transform TeleportToLocation{ get{return _teleportToLocation;}}
    [SerializeField] private GameEvent _playerOnTruck;
    [SerializeField] private GameObject _invisibleBarrier;
    [SerializeField] private GameObject[] _wheelsOnTheBusGoRoundAndRound;
    [SerializeField] private float _maxWheelRotSpeed;
    [SerializeField] private float _wheelRotAccel;
    [SerializeField] private float _currWheelRotSpeed;
    private bool _wheelsSpinning = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if(_wheelsSpinning)
        {
            foreach (GameObject wheel in _wheelsOnTheBusGoRoundAndRound)
            {
                wheel.transform.Rotate(Vector3.back * _currWheelRotSpeed * Time.deltaTime);            
            }
            if(_currWheelRotSpeed >= _maxWheelRotSpeed)
            {
                _currWheelRotSpeed = _maxWheelRotSpeed;
            }
            else if(_currWheelRotSpeed < _maxWheelRotSpeed)
            {
                _currWheelRotSpeed += _wheelRotAccel;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.layer == 3)
        {
            _playerOnTruck.Invoke();
        }
    }

    public void TeleportTruck()
    {
        gameObject.transform.parent.transform.position = _teleportToLocation.position;
        _invisibleBarrier.SetActive(true);
        _wheelsSpinning = true;
    }
}
