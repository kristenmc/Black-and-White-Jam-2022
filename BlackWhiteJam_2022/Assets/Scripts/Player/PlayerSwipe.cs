using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwipe : MonoBehaviour
{
    [SerializeField] private PlayerScript _playerScript;
    [Range(0f, 10f)] [SerializeField] private float _swipeDistance = 2f;
    public void Swipe()
    {
        //TODO: Update to use a boxcast instead
        Debug.Log("swiped");
        RaycastHit2D rayHit;
        rayHit = Physics2D.Raycast(gameObject.transform.position, _playerScript.IsFacing, _swipeDistance, LayerMask.GetMask("Ground", "Platform", "Knockable Object"));
        if(rayHit.collider != null)
        {
            Debug.DrawRay(gameObject.transform.position, _playerScript.IsFacing * _swipeDistance, Color.green, .1f);
            Debug.Log("collision");
            KnockableObject objectHit = rayHit.collider.gameObject.GetComponent<KnockableObject>();
            if(objectHit != null)
            {
                Debug.Log("collision with knockable");
                objectHit.KnockDownObject();
            }
        }
        else
        {
            Debug.DrawRay(gameObject.transform.position, _playerScript.IsFacing * _swipeDistance, Color.red, .1f);
        }
    }
}