using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class HahaTeleportScriptGoBRRRRR : MonoBehaviour
{
    [SerializeField] GameObject _teleportTo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private async void OnTriggerEnter2D(Collider2D other) 
    {
        float _playerDistanceFromPort = gameObject.transform.position.x - other.gameObject.transform.position.x;
        Vector2 oldPosition = other.gameObject.transform.position;
        other.gameObject.transform.position = new Vector2 (_teleportTo.transform.position.x-_playerDistanceFromPort, other.gameObject.transform.position.y);
    }
}
