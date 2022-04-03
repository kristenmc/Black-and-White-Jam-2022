using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int _numCompleteLoops;
    [SerializeField] int _currCompleteLoops;
    [SerializeField] GameObject[] _levelTeleports;
    [SerializeField] int[] _numKnockTargets;
    [SerializeField] int _currKnockTargets = 0;

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
            //Change teleports
            _levelTeleports[_currCompleteLoops - 1].SetActive(false);
            _levelTeleports[_currCompleteLoops].SetActive(true);
            _currCompleteLoops++;
        }

        //Reset tracking variables
        _currKnockTargets = 0;
    }

    public void AddToKnockTarget()
    {
        _currKnockTargets++;
        if(_currKnockTargets >= _numKnockTargets[_currCompleteLoops - 1])
        {
            ProgressLevel();
        }
    }
}
