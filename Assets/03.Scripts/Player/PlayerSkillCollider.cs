using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Player의 스킬 적중!");
            other.GetComponent<EnemyController>().IsHit_skill = true;
        }
    }
}
