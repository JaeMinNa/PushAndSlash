using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // GameManager은 Manager을 관리하는 하나의 역할만 함

    public PlayerManager PlayerManager { get; private set; }
    public UIManager UIManager { get; private set; }
    public DataManager DataManager { get; private set; }
    public SoundManager SoundManager { get; private set; }
    public ScenesManager ScenesManager { get; private set; }

    public static GameManager I;

    private void Awake()
    {
        if (I == null)
        {
            I = this;
        }

        PlayerManager = GetComponentInChildren<PlayerManager>();
        ScenesManager = GetComponentInChildren<ScenesManager>();
        DataManager = GetComponentInChildren<DataManager>();
        SoundManager = GetComponentInChildren<SoundManager>();
        UIManager = GetComponentInChildren<UIManager>();

        Init();
    }

    private void Init()
    {
        PlayerManager.Init();
        DataManager.Init();
        SoundManager.Init();
        ScenesManager.Init();
        UIManager.Init();
    }

    private void Release()
    {
        PlayerManager.Release();
        DataManager.Release();
        SoundManager.Release();
        ScenesManager.Release();
        UIManager.Release();
    }
}