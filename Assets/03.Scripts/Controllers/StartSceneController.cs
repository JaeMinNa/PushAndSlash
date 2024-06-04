using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneController : MonoBehaviour
{
    public void GameStartButton()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        GameManager.I.ScenesManager.LoadLoadingScene("LobbyScene");
    }
}
