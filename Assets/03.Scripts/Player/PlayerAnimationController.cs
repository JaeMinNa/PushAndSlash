using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;
using EpicToonFX;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Transform _shootPosition;
    private Character _character;
    private Animator _animator;
    private CharacterData _playerData;

    private void Start()
    {
        _character = transform.parent.GetComponent<Character>();
        _animator = GetComponent<Animator>();
        _playerData = GameManager.I.DataManager.PlayerData;
    }

    private void Update()
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
