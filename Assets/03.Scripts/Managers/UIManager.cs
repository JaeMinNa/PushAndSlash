using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;
using ECM2.Examples.Slide;

public class UIManager : MonoBehaviour
{
    private GameObject _player;
    private Animator _animator;
    private PlayerCharacter _playerCharacter;

    private void Start()
    {
        _player = GameManager.I.PlayerManager.Player;
        _playerCharacter = _player.GetComponent<PlayerCharacter>();
        _animator = _player.transform.GetChild(0).GetComponent<Animator>();
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
        _animator.SetTrigger("Attack");
    }

    public void PlayerDashButtonUp()
    {
        _playerCharacter.UnCrouch();
    }

    public void PlayerDashButtonDown()
    {
        _playerCharacter.Crouch();
    }
}
