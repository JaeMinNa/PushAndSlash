using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2.Examples.Slide;
using DG.Tweening;

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
    private CameraShake _cameraShake;
    private ParticleSystem _attackParticleSystem;
    private EffectFixedPosition _effectFixedPosition;

    private void Start()
    {
        _player = GameManager.I.PlayerManager.Player;
        _playerCharacter = _player.GetComponent<PlayerCharacter>();
        _enemyController = transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.GetComponent<EnemyController>();
        _cameraShake = Camera.main.GetComponent<CameraShake>();
        _attackParticleSystem = _player.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<ParticleSystem>();
        _effectFixedPosition = _attackParticleSystem.GetComponent<EffectFixedPosition>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(CharacterType == Type.Player)
        {
            if (other.CompareTag("Enemy"))
            {
                StartCoroutine(_cameraShake.COShake(0.8f, 0.3f));
                Vector3 contactPoint = other.ClosestPointOnBounds(transform.position);
                _effectFixedPosition.SetPosition(contactPoint);
                _attackParticleSystem.Play();
                other.GetComponent<EnemyController>().IsHit_attack = true;
            }
        }
        else if(CharacterType == Type.Enemy)
        {
            if (other.CompareTag("Player") && !_playerCharacter.IsSkill)
            {
                StartCoroutine(_cameraShake.COShake(0.8f, 0.3f));
                _player.GetComponent<PlayerCharacter>().PlayerNuckback(transform.position, _enemyController.Atk);
            }
        }
    }
}
