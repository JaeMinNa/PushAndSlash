using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public string CurrentSceneName;

    public void Init()
    {
        CurrentSceneName = SceneManager.GetActiveScene().name;

        if (CurrentSceneName == "BattleSence")
        {
            GameObject playerPrefab = Instantiate(GameManager.I.PlayerManager.PlayerPrefab, Vector3.zero, Quaternion.identity);
            GameManager.I.PlayerManager.Player = playerPrefab;
        }
    }

    public void Release()
    {

    }
}
