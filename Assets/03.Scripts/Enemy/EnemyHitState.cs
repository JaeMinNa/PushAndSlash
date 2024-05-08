using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitState : MonoBehaviour, IEnemyState
{
    private EnemyController _enemyController;
    private float _time;
    private Vector3 _dir;

    // Start���� �����ϰ� ���
    public void Handle(EnemyController enemyController)
    {
        if (!_enemyController)
            _enemyController = enemyController;

        Debug.Log("Hit ���� ����");
        _enemyController.Animator.SetBool("Attack", false);
        _enemyController.Rigidbody.isKinematic = false;
        _time = 0f;
        _dir = (transform.position - _enemyController.Target.transform.position).normalized;
        if(_enemyController.IsHit_attack)
        {
            _enemyController.Rigidbody.velocity = _dir * (_enemyController.PlayerData.Atk - _enemyController.Def);
        }
        else if(_enemyController.IsHit_skill)
        {
            _enemyController.Rigidbody.velocity = _dir * (_enemyController.PlayerData.SkillAtk - _enemyController.Def);
        }
        StartCoroutine(COUpdate());
    }

    // Update���� �����ϰ� ���
    private IEnumerator COUpdate()
    {
        while (true)
        {
            _time += Time.deltaTime;

            if(_enemyController.IsGround())
            {
                if (_time >= 0.5f)
                {
                    _enemyController.IsHit_attack = false;
                    _enemyController.IsHit_skill = false;
                    _enemyController.Rigidbody.isKinematic = true;
                    _enemyController.WalkStart();
                    break;
                }
            }
            else
            {
                if (_time >= 2f)
                {
                    Debug.Log("�� óġ!");
                    break;
                }
            }

            yield return null;
        }
    }
}
