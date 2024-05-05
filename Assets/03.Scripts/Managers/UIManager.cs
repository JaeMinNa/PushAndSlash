using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;
using ECM2.Examples.Slide;

public class UIManager : MonoBehaviour
{
    private GameObject _player;
    private Animator _playerAnimator;
    private PlayerCharacter _playerCharacter;

    private void Start()
    {
        _player = GameManager.I.PlayerManager.Player;
        _playerCharacter = _player.GetComponent<PlayerCharacter>();
        _playerAnimator = _player.transform.GetChild(0).GetComponent<Animator>();
    }

    public void Init()
    {

    }

    public void Release()
    {

    }

    public void PlayerJumpButtonUp()
    {
        _player.GetComponent<Character>().StopJumping();
    }

    public void PlayerJumpButtonDown()
    {
        _player.GetComponent<Character>().Jump();
    }

    public void PlayerAttack()
    {
        _playerAnimator.SetTrigger("Attack");
    }

    public void PlayerDashButtonUp()
    {
        _playerCharacter.UnCrouch();
    }

    public void PlayerDashButtonDown()
    {
        _playerAnimator.SetTrigger("Dash");
        _playerCharacter.Crouch();
    }
}
