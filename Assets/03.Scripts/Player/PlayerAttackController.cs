using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] private BoxCollider[] _weaponColliders;
    [SerializeField] private BoxCollider _skillCollider;

    public void AttackColliderActive(float time)
    {
        for (int i = 0; i < _weaponColliders.Length; i++)
        {
            _weaponColliders[i].enabled = true;
        }

        StartCoroutine(COAttackColliderInactive(time));
    }

    private IEnumerator COAttackColliderInactive(float time)
    {
        yield return new WaitForSeconds(time);

        for (int i = 0; i < _weaponColliders.Length; i++)
        {
            _weaponColliders[i].enabled = false;
        }
    }

    public void SkillColliderActive(float time)
    {

        _skillCollider.enabled = true;
    
        StartCoroutine(COSkillColliderInactive(time));
    }

    private IEnumerator COSkillColliderInactive(float time)
    {
        yield return new WaitForSeconds(time);

        _skillCollider.enabled = false;
    }
}
