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
            GameObject playerPrefab = Instantiate(Resources.Load<GameObject>("Prefabs/Characters/" + GameManager.I.DataManager.PlayerData.Tag), Vector3.zero, Quaternion.identity);
            GameManager.I.PlayerManager.Player = playerPrefab;
        }
    }

    public void Release()
    {

    }
}
