using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : MonoBehaviour, IEnemyState
{
    private EnemyController _enemyController;
    private float _time;

    // Start문과 동일하게 사용
    public void Handle(EnemyController enemyController)
    {
        if (!_enemyController)
            _enemyController = enemyController;

        Debug.Log("Attack 상태 시작");
        _time = 0f;
        _enemyController.NavMeshAgent.isStopped = true;
        _enemyController.NavMeshAgent.velocity = Vector3.zero;
        StartCoroutine(COUpdate());
    }

    // Update문과 동일하게 사용
    private IEnumerator COUpdate()
    {
        while (true)
        {
            _time += Time.deltaTime;

            if(_time >= _enemyController.EnemyData.AttackCoolTime)
            {
                if(_enemyController.CheckPlayer())
                {
                    _enemyController.AttackStart();
                    _enemyController.Animator.SetTrigger("ReAttack");
                    break;
                }
                else
                {
                    _enemyController.WalkStart();
                    _enemyController.Animator.SetBool("Attack", false);
                    break;
                }
            }

            yield return null;
        }
    }
}
