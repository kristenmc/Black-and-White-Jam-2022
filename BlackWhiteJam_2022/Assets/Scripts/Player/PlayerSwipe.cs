using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwipe : MonoBehaviour
{
    [Range(0f, 50f)] [SerializeField] private float _swipeDistance = 2f;
    public void Swipe()
    {
        //TODO: Update to use a boxcast instead
        Debug.Log("swiped");
        RaycastHit2D rayHit;
        //Determine player direction for raycast direction
        //Vector2 swipeDirection = _direction.magnitude > 0.1f ? swipeDirection = Vector2.right : swipeDirection = Vector2.left;
        Vector2 swipeDirection = Vector3.right;
        rayHit = Physics2D.Raycast(gameObject.transform.position, swipeDirection, _swipeDistance, LayerMask.GetMask("Ground"));
        if(rayHit.collider != null)
        {
            Debug.DrawRay(gameObject.transform.position, swipeDirection * _swipeDistance, Color.green, .1f);
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
            Debug.DrawRay(gameObject.transform.position, swipeDirection * _swipeDistance, Color.red, .1f);
        }
    }
}
