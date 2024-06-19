using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartSceneController : MonoBehaviour
{
    [SerializeField] private GameObject _loginGoogleButton;
    [SerializeField] private GameObject _startButton;
    [SerializeField] private TMP_Text _idText;

    private void Update()
    {
        _idText.text = "GPGS UserID : " + GameManager.I.GPGSManager.GetGPGSUserID();
    }

    public void Save()
    {
        GameManager.I.BackendManager.Save();
    }

    public void Load()
    {
        GameManager.I.BackendManager.Load();
    }

    public void GameStartButton()
    {
        //if (GameManager.I.GPGSManager.GetGPGSUserID() == "0")
        //{
        //    GameManager.I.SoundManager.StartSFX("ButtonClickMiss");
        //}
        //else
        //{
        //    GameManager.I.DataManager.DataSave();
        //    GameManager.I.SoundManager.StartSFX("ButtonClick");
        //    GameManager.I.ScenesManager.LoadLoadingScene("LobbyScene");
        //}

        GameManager.I.DataManager.DataSave();
        GameManager.I.ScenesManager.LoadLoadingScene("LobbyScene");
    }
}
