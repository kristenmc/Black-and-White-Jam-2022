using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserDetectionScript : MonoBehaviour
{
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
        if(other.gameObject.layer == 3)
        {
            Debug.Log("found cat");
            gameObject.GetComponentInParent<ChaserScript>().DetectPlayer(other.gameObject);
        }     
    }
}