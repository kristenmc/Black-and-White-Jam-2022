using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _numCompleteLoops;
    [SerializeField] private int _currCompleteLoops;
    [SerializeField] private GameObject[] _levelTeleports;
    [SerializeField] private int[] _numKnockTargets;
    [SerializeField] private KnockableObject[] _inactiveKnockables;
    [SerializeField] private int _currKnockTargets = 0;
    [SerializeField] private IceCreamTruckScript _iceCreamTruck;
    [SerializeField] private int _iceCreamLoopNum = -1;
    [SerializeField] private GameObject _playerChar;
    [SerializeField] private IceCreamBackgroundLooper _iceCreamLoopingBackground;
    private bool _teleportedTruckAlready = false;
    [SerializeField] float _gameTimer;
    public float GameTimer{get{return _gameTimer;}}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() 
    {
        //count down timer
        if(_gameTimer <= 0)
        {
            LoseGame();
        }   
    }

    public void ProgressLevel()
    {
        //Potentially replace this later or add in the level shifts
        if(_currCompleteLoops < _levelTeleports.Length-1)
        {
            //Change teleports and knockables
            _levelTeleports[_currCompleteLoops].SetActive(false);
            _currCompleteLoops++;
            _levelTeleports[_currCompleteLoops].SetActive(true);
            foreach(KnockableObject knockable in _inactiveKnockables)
            {
                knockable.CanBeKnocked = true;
            }
        }
        else if(_currCompleteLoops >= _levelTeleports.Length-1)
        {
            //add transition to game end right here
            WinGame();
            Debug.Log("end the game");
        }

        //Reset tracking variables
        _currKnockTargets = 0;
    }

    public void AddToKnockTarget()
    {
        _currKnockTargets++;
        if(_currKnockTargets >= _numKnockTargets[_currCompleteLoops])
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
                float teleportXDistance = _iceCreamTruck.TeleportToLocation.position.x - _iceCreamTruck.transform.position.x;
                _playerChar.transform.position = new Vector2(_playerChar.transform.position.x + teleportXDistance, _playerChar.transform.position.y);
                _iceCreamTruck.TeleportTruck();
                _teleportedTruckAlready = true;
                foreach(GameObject teleport in _levelTeleports)
                {
                    teleport.SetActive(false);
                }
            }
            if(_iceCreamLoopingBackground != null)
            {
                _iceCreamLoopingBackground.BeginLooping();
            }
        }
    }

    public void WinGame()
    {

    }
    
    public void LoseGame()
    {

    }
}
