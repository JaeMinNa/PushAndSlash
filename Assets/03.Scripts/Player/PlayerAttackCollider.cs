using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            Debug.Log("PlayerÀÇ °ø°Ý!");
            other.GetComponent<EnemyController>().IsHit_attack = true;
        }
    }
}
