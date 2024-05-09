using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] private Transform _shootPosition;
    public GameObject Skill;
    public void StartSFX(string name)
    {
        GameManager.I.SoundManager.StartSFX(name);
    }

    public void ShootRangedAttack()
    {
        Instantiate(Skill, _shootPosition.position, Quaternion.identity);
    }
}
