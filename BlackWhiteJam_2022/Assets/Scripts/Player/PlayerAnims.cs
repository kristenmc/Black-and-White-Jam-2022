using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnims : MonoBehaviour
{
    [SerializeField] private PlayerScript _playerScript;

    [SerializeField] string _idle;
    [SerializeField] string _move;
    [SerializeField] string _liquifyBox;
    [SerializeField] string _liquifySwing;
    [SerializeField] string _attack;
    [SerializeField] string _jumpUp;
    [SerializeField] string _fallDown;
    [SerializeField] string _land;    

    public void PlayIdleAnim()
    {
        _playerScript.Animator.Play(_idle);
    }

    public void PlayMoveAnim()
    {
        _playerScript.Animator.Play(_move);
    }

    public void PlayLiquifyBoxAnim()
    {
        _playerScript.Animator.Play(_liquifyBox);
    }

    public void PlayLiquifySwingAnim()
    {
        _playerScript.Animator.Play(_liquifySwing);
    }

    public void PlayAttackAnim()
    {
        _playerScript.Animator.Play(_attack);
    }

    public void PlayJumpUp()
    {
        _playerScript.Animator.Play(_jumpUp);
    }
    
    public void PlayFallDown()
    {
        _playerScript.Animator.Play(_fallDown);
    }
    
    public void PlayLand()
    {
        _playerScript.Animator.Play(_land);
    }
}
