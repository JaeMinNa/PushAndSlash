using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2.Examples.Slide;
using Photon.Pun;
using Photon.Realtime;

public class SkillCollider : MonoBehaviour
{
    private CameraShake _cameraShake;
    private CharacterData _playerData;
    private PlayerCharacter _playerCharacter;
    private PhotonView _photonView;

    private void Start()
    {
        _cameraShake = Camera.main.GetComponent<CameraShake>();
        _playerData = GameManager.I.DataManager.PlayerData;
        _playerCharacter = transform.parent.GetComponent<PlayerCharacter>();
        _photonView = transform.parent.GetComponent<PhotonView>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            StartCoroutine(_cameraShake.COShake(0.8f, 0.5f));
            other.GetComponent<EnemyController>().IsHit_skill = true;
        }
        else if (other.CompareTag("Player") && !other.gameObject.Equals(_playerCharacter.gameObject))
        {
            StartCoroutine(_cameraShake.COShake(0.8f, 0.5f));

            if(_photonView.IsMine) other.GetComponent<PlayerCharacter>().PlayerNuckback(transform.position, _playerData.SkillAtk);
            else other.GetComponent<PlayerCharacter>().PlayerNuckback(transform.position, _playerCharacter.SkillAtk);
        }
    }
}
