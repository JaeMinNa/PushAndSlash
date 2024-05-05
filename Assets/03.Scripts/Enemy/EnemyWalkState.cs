using System.Collections;
using UnityEngine;

public class EnemyWalkState : MonoBehaviour, IEnemyState
{
    private EnemyController _enemyController;

    // Start문과 동일하게 사용
    public void Handle(EnemyController enemyController)
    {
        if (!_enemyController)
            _enemyController = enemyController;

        Debug.Log("Walk 상태 시작");
        _enemyController.NavMeshAgent.isStopped = false;
        StartCoroutine(COUpdate());
    }

    // Update문과 동일하게 사용
    private IEnumerator COUpdate()
    {
        while (true)
        { 
            _enemyController.NavMeshAgent.SetDestination(_enemyController.Target.transform.position);

            if(_enemyController.CheckPlayer())
            {
                _enemyController.AttackStart();
                _enemyController.Animator.SetBool("Attack", true);
                break;
            }

            yield return null;
        }
    }
}