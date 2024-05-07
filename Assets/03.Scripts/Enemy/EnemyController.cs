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
    [HideInInspector] public Rigidbody Rigidbody;
    [HideInInspector] public RaycastHit ForwardHit;
    [HideInInspector] public RaycastHit DownHit;
    [HideInInspector] public CharacterData PlayerData;
    [HideInInspector] public float Speed;
    [HideInInspector] public float Atk;
    [HideInInspector] public float Def;
    public bool IsHit_attack;
    public bool IsHit_skill;

    private IEnemyState _walkState;
    private IEnemyState _attackState;
    private IEnemyState _hitState;

    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        Animator = transform.GetChild(0).GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Target = GameManager.I.PlayerManager.Player;
        PlayerData = GameManager.I.DataManager.PlayerData;

        _enemyStateContext = new EnemyStateContext(this);
        _walkState = gameObject.AddComponent<EnemyWalkState>();
        _attackState = gameObject.AddComponent<EnemyAttackState>();
        _hitState = gameObject.AddComponent<EnemyHitState>();

        _enemyStateContext.Transition(_walkState);

        EnemySetting();
        IsHit_attack = false;
    }

    private void Update()
    {
        CheckPlayer();
        transform.LookAt(Target.transform.position);
        Debug.Log(IsGround());
    }

    public void WalkStart()
    {
        _enemyStateContext.Transition(_walkState);
    }

    public void AttackStart()
    {
        _enemyStateContext.Transition(_attackState);
    }

    public void HitStart()
    {
        _enemyStateContext.Transition(_hitState);
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

        Speed = EnemyData.Speed;
        Atk = EnemyData.Atk;
        Def = EnemyData.Def;
    }

    public bool CheckPlayer()
    {
        Debug.DrawRay(transform.position + new Vector3(0, 0.7f, 0), transform.forward * 1.3f, Color.green);

        if (Physics.Raycast(transform.position + new Vector3(0, 0.7f, 0), transform.forward, out ForwardHit, 1.3f))
        { 
            if (ForwardHit.transform.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }

    public bool IsGround()
    {
        Debug.DrawRay(transform.position + new Vector3(0, 0.5f, 0), Vector3.down, Color.red);

        if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), Vector3.down, out DownHit, 1f))
        {
            if (DownHit.transform.CompareTag("Ground"))
            {
                return true;
            }
        }

        return false;
    }
}