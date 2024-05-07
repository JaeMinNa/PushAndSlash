using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Player�� ��ų ����!");
            other.GetComponent<EnemyController>().IsHit_skill = true;
        }
    }
}
