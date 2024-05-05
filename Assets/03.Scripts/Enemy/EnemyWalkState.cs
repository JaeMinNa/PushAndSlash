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
        StartCoroutine(COUpdate());
    }

    // Update문과 동일하게 사용
    private IEnumerator COUpdate()
    {
        while (true)
        {
            //Vector3 dir = (_enemyController.Target.transform.position - transform.position).normalized;
            //transform.position += dir * _enemyController.EnemyData.Speed * Time.deltaTime;

            _enemyController.NavMeshAgent.SetDestination(_enemyController.Target.transform.position);

            yield return null;
        }
    }
}