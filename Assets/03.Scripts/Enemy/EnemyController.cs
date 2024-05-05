using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public enum EnemyType
    {
        Enemy0,
        Enemy1,
        Enemy2,
    }

    public EnemyStateContext _enemyStateContext { get; private set; }

    public EnemyType Type;
    [HideInInspector] public GameObject Target;
    [HideInInspector] public EnemyData EnemyData;
    [HideInInspector] public NavMeshAgent NavMeshAgent;

    private IEnemyState _walkState;

    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        Target = GameManager.I.PlayerManager.Player;

        _enemyStateContext = new EnemyStateContext(this);
        _walkState = gameObject.AddComponent<EnemyWalkState>();

        _enemyStateContext.Transition(_walkState);

        EnemySetting();
    }

    public void WalkStart()
    {
        _enemyStateContext.Transition(_walkState);
    }

    private void EnemySetting()
    {
        switch (Type)
        {
            case EnemyType.Enemy0:
                EnemyData = GameManager.I.DataManager.DataWrapper.EnemyDatas[0];
                break;

            case EnemyType.Enemy1:
                EnemyData = GameManager.I.DataManager.DataWrapper.EnemyDatas[1];
                break;

            case EnemyType.Enemy2:
                EnemyData = GameManager.I.DataManager.DataWrapper.EnemyDatas[2];
                break;

            default:
                break;
        }

        NavMeshAgent.speed = EnemyData.Speed;
    }
}