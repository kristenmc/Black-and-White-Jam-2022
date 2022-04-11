using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLiquify : MonoBehaviour
{
    [SerializeField] private PlayerScript _playerScript;
    [SerializeField] private float _liquifyCheckSizeX;
    [SerializeField] private float _liquifyCheckSizeY;

    public void Liquify()
    {
        Debug.Log("Player Liquify");
        RaycastHit2D rayHit;
        rayHit = Physics2D.BoxCast(gameObject.transform.position, new Vector2(_liquifyCheckSizeX, _liquifyCheckSizeY), 0f, Vector2.down, _liquifyCheckSizeY, LayerMask.GetMask("Liquify Object"));
        if(rayHit.collider != null && !_playerScript.Liquified)
        {
            Debug.DrawRay(gameObject.transform.position, Vector2.down * _liquifyCheckSizeY, Color.green, .1f);
            transform.position = rayHit.collider.transform.position;
            _playerScript.Liquified = true;
            Debug.Log("player is liquid");
        }
        else
        {
            Debug.DrawRay(gameObject.transform.position, Vector2.down * _liquifyCheckSizeY, Color.red, .1f);
        }
    }
}
