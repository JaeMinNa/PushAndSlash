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
    [SerializeField] private SkinnedMeshRenderer _renderer;
    [HideInInspector] public float Atk;
    private GameObject _player;
    private Vector3 _dir;
    private ParticleSystem _effect;
    private CameraShake _cameraShake;

    private void Awake()
    {
        _effect = transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>();
        _cameraShake = Camera.main.transform.GetComponent<CameraShake>();
    }

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
                StartCoroutine(_cameraShake.COShake(0.3f, 0.3f));
                _effect.Play();
                _renderer.enabled = false;
                //Destroy(gameObject);
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
