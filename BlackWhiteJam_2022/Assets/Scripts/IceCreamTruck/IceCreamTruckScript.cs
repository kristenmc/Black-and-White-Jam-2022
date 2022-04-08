using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreamTruckScript : MonoBehaviour
{
    [SerializeField] private Transform _teleportToLocation;
    public Transform TeleportToLocation{ get{return _teleportToLocation;}}
    [SerializeField] private GameEvent _playerOnTruck;
    [SerializeField] private GameObject _invisibleBarrier;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Debug.Log("found the cat");
        _playerOnTruck.Invoke();
    }

    public void TeleportTruck()
    {
        gameObject.transform.parent.transform.position = _teleportToLocation.position;
        _invisibleBarrier.SetActive(true);
    }
}
