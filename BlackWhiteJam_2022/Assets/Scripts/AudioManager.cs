using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static private AudioManager _instance;
    static public AudioManager Instance { get { return _instance;}}
    [SerializeField] private string[] _loopEvents;
    private FMOD.Studio.EventInstance[] _fmodInstances;
    private int _index = 0;
    
    private void Start()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
            _fmodInstances = new FMOD.Studio.EventInstance[_loopEvents.Length];
            for(int i = 0; i < _loopEvents.Length; i++)
            {
                _fmodInstances[i] = FMODUnity.RuntimeManager.CreateInstance(_loopEvents[i]);
            }
            _index = 0;
            NextLoopMusic();
        }
        else
        {
            Destroy(this.gameObject);
        }

        
        

    }

    public void NextLoopMusic()
    {
        if(_index < _fmodInstances.Length)
        {
            _fmodInstances[_index].start();
            if(_index >0 )
            {
                _fmodInstances[_index-1].stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Level_Loop"); //Play sound to indicate new loop
            }
            _index++;
        }
        
    }

    public void ResetMusic()
    {
        if(_index == 0)
        {
            _fmodInstances[0].stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        if(_index <= _fmodInstances.Length)
        {
            _fmodInstances[_index-1].stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            _index = 0;
            NextLoopMusic();
        }
    }
}
