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
    [HideInInspector] public Animator Animator;
    [HideInInspector] public RaycastHit hit;

    private IEnemyState _walkState;
    private IEnemyState _attackState;

    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        Animator = transform.GetChild(0).GetComponent<Animator>();
    }

    private void Start()
    {
        Target = GameManager.I.PlayerManager.Player;

        _enemyStateContext = new EnemyStateContext(this);
        _walkState = gameObject.AddComponent<EnemyWalkState>();
        _attackState = gameObject.AddComponent<EnemyAttackState>();

        _enemyStateContext.Transition(_walkState);

        EnemySetting();
    }

    private void Update()
    {
        CheckPlayer();
    }

    public void WalkStart()
    {
        _enemyStateContext.Transition(_walkState);
    }

    public void AttackStart()
    {
        _enemyStateContext.Transition(_attackState);
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

    public bool CheckPlayer()
    {
        Debug.DrawRay(transform.position + new Vector3(0, 0.7f, 0), transform.forward * 1.5f, Color.green);

        if (Physics.Raycast(transform.position + new Vector3(0, 0.7f, 0), transform.forward, out hit, 1.5f))
        { 
            if (hit.transform.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }
}