using System.Collections;
using UnityEngine;

public class EnemyWalkState : MonoBehaviour, IEnemyState
{
    private EnemyController _enemyController;

    // Start���� �����ϰ� ���
    public void Handle(EnemyController enemyController)
    {
        if (!_enemyController)
            _enemyController = enemyController;

        Debug.Log("Walk ���� ����");
        _enemyController.NavMeshAgent.isStopped = false;
        StartCoroutine(COUpdate());
    }

    // Update���� �����ϰ� ���
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