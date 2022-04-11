using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private float _currTime = 0;
    [Range(1, 2)][SerializeField] private float _timerSpeedMultiplier;
    [SerializeField] private float _maxTime;
    [SerializeField] private Image _timerFill;
    [SerializeField] private Image _timerHandle;
    public float CurrentTime{get{return _currTime;}}
    [SerializeField] private Slider _progressBar;
    [SerializeField] private GameEvent _gameWinEvent; 
    [SerializeField] private GameEvent _gameLoseEvent;

    // Start is called before the first frame update
    void Start()
    {
        _progressBar.minValue = 0;
        _progressBar.maxValue = 0;
        foreach(int knockable in _numKnockTargets)
        {
            _progressBar.maxValue += knockable;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() 
    {
        _timerFill.fillAmount = _currTime/_maxTime;
        _timerHandle.transform.eulerAngles = new Vector3(0, 0, -_currTime/_maxTime * 360);
        
        if(_currCompleteLoops == 0)
        {
            _currTime += Time.deltaTime;
        }
        else
        {
            _currTime += Time.deltaTime * _timerSpeedMultiplier * _currCompleteLoops;
        }
        //count down timer
        if(_currTime >= _maxTime)
        {
            LoseGame();
        }   
    }

    public void ProgressLevel()
    {
        //Potentially replace this later or add in the level shifts
        if(_currCompleteLoops < _levelTeleports.Length-1)
        {
            //Change the music to go faster. Do Note this will be called multiple times (twice)
            AudioManager.Instance.NextLoopMusic();
            
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
        _progressBar.value++;
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
        _gameWinEvent.Invoke();
    }
    
    public void LoseGame()
    {
        _gameLoseEvent.Invoke();
    }
}
