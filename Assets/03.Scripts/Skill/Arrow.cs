using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2.Examples.Slide;

public class Arrow : MonoBehaviour
{
    public enum Type
    {
        Player,
        Enemy,
    }

    public Type CharacterType;
    [SerializeField] private float _speed;
    [HideInInspector] public float Atk;
    private GameObject _player;
    private Vector3 _dir;

    private void Start()
    {
        _player = GameManager.I.PlayerManager.Player;
        _dir = (_player.transform.position + new Vector3(0, 0.5f, 0) - transform.position).normalized;
        transform.LookAt(_player.transform.position + new Vector3(0, 0.5f, 0));

        StartCoroutine(CODestroyAttack());
    }

    private void Update()
    {
        if (CharacterType == Type.Enemy)
        {
            transform.position += _dir * _speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(CharacterType == Type.Enemy)
        {
            if (other.CompareTag("Player"))
            {
                _player.GetComponent<PlayerCharacter>().PlayerNuckback(transform.position, Atk);
                GameManager.I.SoundManager.StartSFX("ArrowHit");
                Destroy(gameObject);
            }
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
