using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // GameManager�� Manager�� �����ϴ� �ϳ��� ���Ҹ� ��

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
        UIManager.Init();
        DataManager.Init();
        SoundManager.Init();
        ScenesManager.Init();
    }

    private void Release()
    {
        PlayerManager.Release();
        UIManager.Release();
        DataManager.Release();
        SoundManager.Release();
        ScenesManager.Release();
    }
}