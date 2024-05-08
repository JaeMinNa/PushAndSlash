using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("CameraAudioSource")]
    [SerializeField] private AudioSource _cameraBGMAudioSource;
    [SerializeField] private AudioSource _cameraSFXAuidoSource;

    [Header("ETCAudioSource")]
    [SerializeField] private AudioSource[] _etcSFXAudioSources;

    private Dictionary<string, AudioClip> _bgm;
    private Dictionary<string, AudioClip> _sfx;
    private int _index;
    [SerializeField] private float _maxDistance = 50f;
    [Range(0f, 1f)] public float StartVolume = 0.1f;

    public void Init()
    {
        // �ʱ� ����
        _bgm = new Dictionary<string, AudioClip>();
        _sfx = new Dictionary<string, AudioClip>();

        _cameraBGMAudioSource.loop = true;
        _cameraBGMAudioSource.volume = StartVolume;
        _cameraSFXAuidoSource.playOnAwake = false;
        _cameraSFXAuidoSource.volume = StartVolume;
        for (int i = 0; i < _etcSFXAudioSources.Length; i++)
        {
            _etcSFXAudioSources[i].playOnAwake = false;
            _etcSFXAudioSources[i].volume = StartVolume;
        }

        // BGM

        // SFX
        _sfx.Add("PlayerDash", Resources.Load<AudioClip>("Sounds/SFX/Player/PlayerDash"));
        _sfx.Add("PlayerHit", Resources.Load<AudioClip>("Sounds/SFX/Player/PlayerHit"));
        _sfx.Add("PlayerJump", Resources.Load<AudioClip>("Sounds/SFX/Player/PlayerJump"));
        _sfx.Add("PlayerJumpEnd", Resources.Load<AudioClip>("Sounds/SFX/Player/PlayerJumpEnd"));
        _sfx.Add("PlayerSwordAttack0", Resources.Load<AudioClip>("Sounds/SFX/Player/PlayerSwordAttack0"));
        _sfx.Add("PlayerSwordAttack1", Resources.Load<AudioClip>("Sounds/SFX/Player/PlayerSwordAttack1"));
        _sfx.Add("PlayerSwordSkill", Resources.Load<AudioClip>("Sounds/SFX/Player/PlayerSwordSkill"));
        _sfx.Add("EnemyStickAttack", Resources.Load<AudioClip>("Sounds/SFX/Enemy/EnemyStickAttack"));
    }

    // �޸� ����
    public void Release()
    {

    }

    // �ٸ� ������Ʈ���� ��µǴ� ����
    // 2D������ Vector2.Distance ���
    public void StartSFX(string name, Vector3 position)
    {
        _index = _index % _etcSFXAudioSources.Length;

        float distance = Vector3.Distance(position, GameManager.I.PlayerManager.Player.transform.position);
        float volume = 1f - (distance / _maxDistance);
        _etcSFXAudioSources[_index].volume = Mathf.Clamp01(volume) * StartVolume;
        _etcSFXAudioSources[_index].PlayOneShot(_sfx[name]);

        _index++;
    }

    // Player���� ��µǴ� ����
    public void StartSFX(string name)
    {
        _cameraSFXAuidoSource.PlayOneShot(_sfx[name]);
    }

    public void StartBGM(string name)
    {
        _cameraBGMAudioSource.Stop();
        _cameraBGMAudioSource.clip = _bgm[name];
        _cameraBGMAudioSource.Play();
    }

    public void StopBGM(string name)
    {
        if (_cameraBGMAudioSource != null) _cameraBGMAudioSource.Stop();
    }
}
