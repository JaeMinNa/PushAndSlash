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
        StartCoroutine(COUpdate());
    }

    // Update���� �����ϰ� ���
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