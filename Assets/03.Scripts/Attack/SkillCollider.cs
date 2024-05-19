using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCollider : MonoBehaviour
{
    private CameraShake _cameraShake;

    private void Start()
    {
        _cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            StartCoroutine(_cameraShake.COShake(0.8f, 0.5f));
            other.GetComponent<EnemyController>().IsHit_skill = true;
        }
    }
}
