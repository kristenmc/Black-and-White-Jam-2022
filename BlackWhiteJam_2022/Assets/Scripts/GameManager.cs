using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _numCompleteLoops;
    [SerializeField] private int _currCompleteLoops;
    [SerializeField] private GameObject[] _levelTeleports;
    [SerializeField] private int[] _numKnockTargets;
    [SerializeField] private GameObject[] _levelKnockables;
    [SerializeField] private int _currKnockTargets = 0;
    [SerializeField] private IceCreamTruckScript _iceCreamTruck;
    [SerializeField] private int _iceCreamLoopNum = -1;
    [SerializeField] private GameObject _playerChar;
    [SerializeField] private IceCreamBackgroundLooper _iceCreamLoopingBackground;
    private bool _teleportedTruckAlready = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ProgressLevel()
    {
        //Potentially replace this later or add in the level shifts
        if(_currCompleteLoops <= _levelTeleports.Length)
        {
            //Change teleports and knockables
            _levelTeleports[_currCompleteLoops].SetActive(false);
            _currCompleteLoops++;
            _levelTeleports[_currCompleteLoops].SetActive(true);
            _levelKnockables[_currCompleteLoops].SetActive(true);
        }

        //Reset tracking variables
        _currKnockTargets = 0;
    }

    public void AddToKnockTarget()
    {
        _currKnockTargets++;
        if(_currCompleteLoops >= _numCompleteLoops)
        {
            Debug.Log("End Level");
        }
        else if(_currKnockTargets >= _numKnockTargets[_currCompleteLoops])
        {
            ProgressLevel();
        }
    }

    public void AttemptToActivateIceCreamTruck()
    {
        if(_iceCreamTruck != null && _currCompleteLoops == _iceCreamLoopNum)
        {
            if(!_teleportedTruckAlready)
            {
                float teleportYDistance = _iceCreamTruck.TeleportToLocation.position.y - _iceCreamTruck.transform.position.y;
                _playerChar.transform.position = new Vector2(_playerChar.transform.position.x, _playerChar.transform.position.y + teleportYDistance);
                _iceCreamTruck.TeleportTruck();
                _teleportedTruckAlready = true;
            }
            if(_iceCreamLoopingBackground != null)
            {
                _iceCreamLoopingBackground.BeginLooping();
            }
        }
    }
}
