using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2.Examples.Slide;

public class AttackCollider : MonoBehaviour
{
    public enum Type
    {
        Player,
        Enemy,
    }

    public Type CharacterType;

    private GameObject _player;
    private EnemyController _enemyController;
    private PlayerCharacter _playerCharacter;

    private void Start()
    {
        _player = GameManager.I.PlayerManager.Player;
        _playerCharacter = _player.GetComponent<PlayerCharacter>();

        if (CharacterType == Type.Enemy)
        {
            Transform topParent = transform;
            while (topParent.parent != null)
            {
                topParent = topParent.parent;
            }

            _enemyController = topParent.GetComponent<EnemyController>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(CharacterType == Type.Player)
        {
            if (other.CompareTag("Enemy"))
            {
                Debug.Log("Player의 공격!");
                other.GetComponent<EnemyController>().IsHit_attack = true;
            }
        }
        else if(CharacterType == Type.Enemy)
        {
            if (other.CompareTag("Player") && !_playerCharacter.IsSkill)
            {
                Debug.Log("Enemy의 공격!");
                _player.GetComponent<PlayerCharacter>().PlayerNuckback(this.transform, _enemyController.Atk);
            }
        }
    }
}
