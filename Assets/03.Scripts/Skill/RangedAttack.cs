using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    //public enum Type
    //{
    //    Player,
    //    Enemy,
    //}

    //public Type CharacterType;

    [SerializeField] private float _speed;
    private GameObject _player;
    private Vector3 _dir;

    private void Start()
    {
        _player = GameManager.I.PlayerManager.Player;
        _dir = (_player.transform.position - transform.position).normalized;
        transform.LookAt(_player.transform.position);

        StartCoroutine(CODestroyAttack());
    }

    private void Update()
    {
        transform.position += _dir * _speed * Time.deltaTime;
    }

    //public void EnemyShoot()
    //{

    //}

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("스킬 Player 적중!");
            Destroy(gameObject);
        }
    }

    private IEnumerator CODestroyAttack()
    {
        yield return new WaitForSeconds(5f);
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }
}
