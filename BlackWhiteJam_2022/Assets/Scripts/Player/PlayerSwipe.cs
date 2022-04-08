using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwipe : MonoBehaviour
{
    [SerializeField] private PlayerScript _playerScript;
    [Range(0f, 10f)] [SerializeField] private float _swipeDistance = 2f;
    [Range(0f, 3f)] [SerializeField] private float _swipeSize = .7f;
    public void Swipe()
    {
        //TODO: Update to use a boxcast instead
        RaycastHit2D rayHit;
        rayHit = Physics2D.BoxCast(gameObject.transform.position, new Vector2(_swipeSize, _swipeSize), 0f,_playerScript.IsFacing, _swipeDistance, LayerMask.GetMask("Ground", "Platform", "Knockable Object"));
        if(rayHit.collider != null)
        {
            Debug.DrawRay(gameObject.transform.position, _playerScript.IsFacing * _swipeDistance, Color.green, .1f);
            KnockableObject objectHit = rayHit.collider.gameObject.GetComponent<KnockableObject>();
            if(objectHit != null && !objectHit.IsKnocked)
            {
                objectHit.KnockDownObject();
            }
        }
        else
        {
            Debug.DrawRay(gameObject.transform.position, _playerScript.IsFacing * _swipeDistance, Color.red, .1f);
        }
    }
}
