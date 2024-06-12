using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;
using EpicToonFX;
using Photon.Pun;
using Photon.Realtime;
using ECM2.Examples.Slide;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Transform _shootPosition;
    private Character _character;
    private Animator _animator;
    private CharacterData _playerData;
    private PlayerCharacter _playerCharacter;

    private void Start()
    {
        _character = transform.parent.GetComponent<Character>();
        _playerCharacter = transform.parent.GetComponent<PlayerCharacter>();
        _animator = GetComponent<Animator>();
        _playerData = GameManager.I.DataManager.PlayerData;
    }

    private void Update()
    {
        if (GameManager.I.ScenesManager.CurrentSceneName == "BattleScene1")
        {
            if (_character.IsGrounded())
            {
                _animator.SetBool("Ground", true);
                _animator.SetBool("Jump", false);

                if (_character.GetSpeed() > 0) _animator.SetBool("Run", true);
                else _animator.SetBool("Run", false);
            }
            else
            {
                _animator.SetBool("Ground", false);
                _animator.SetBool("Jump", true);
            }

            //if (_character.IsGrounded())
            //{
            //    _playerCharacter.SetBoolGround(true);
            //    _playerCharacter.SetBoolJump(false);

            //    if (_character.GetSpeed() > 0) _playerCharacter.SetBoolRun(true);
            //    else _playerCharacter.SetBoolRun(false);
            //}
            //else
            //{
            //    _playerCharacter.SetBoolGround(false);
            //    _playerCharacter.SetBoolJump(true);
            //}
        }
        else if (GameManager.I.ScenesManager.CurrentSceneName == "MultiBattleScene1")
        {
            if(GameManager.I.PlayerManager.Player.GetComponent<PhotonView>().IsMine)
            {
                if (_character.IsGrounded())
                {
                    _animator.SetBool("Ground", true);
                    _animator.SetBool("Jump", false);

                    if (_character.GetSpeed() > 0) _animator.SetBool("Run", true);
                    else _animator.SetBool("Run", false);
                }
                else
                {
                    _animator.SetBool("Ground", false);
                    _animator.SetBool("Jump", true);
                }

                //if (_character.IsGrounded())
                //{
                //    _playerCharacter.SetBoolGround(true);
                //    _playerCharacter.SetBoolJump(false);

                //    if (_character.GetSpeed() > 0) _playerCharacter.SetBoolRun(true);
                //    else _playerCharacter.SetBoolRun(false);
                //}
                //else
                //{
                //    _playerCharacter.SetBoolGround(false);
                //    _playerCharacter.SetBoolJump(true);
                //}
            }
        }
    }

    public void StartSFX(string name)
    {
        GameManager.I.SoundManager.StartSFX(name);
    }

    public void ShootRangedAttack(string name)
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/Skills/Player/" + name), _shootPosition.position, Quaternion.identity);

        //if (_enemyController.Type == EnemyController.EnemyType.Enemy4)
        //{
        //    obj.GetComponent<ETFXProjectileScript>().Atk = _enemyController.Atk;
        //}
        //else if (_enemyController.Type == EnemyController.EnemyType.Enemy5)
        //{
        //    obj.GetComponent<Arrow>().Atk = _enemyController.Atk;
        //}

        obj.GetComponent<ETFXProjectileScript>().Atk = _playerData.SkillAtk;
    }

    public void ShootArrowAttack(string name)
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/Skills/Player/" + name), _shootPosition.position, Quaternion.identity);
    }
}
