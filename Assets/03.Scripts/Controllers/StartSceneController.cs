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

    public void GameStartButton()
    {
        if(GameManager.I.GPGSManager.GetGPGSUserID() == "0")
        {
            GameManager.I.SoundManager.StartSFX("ButtonClickMiss");
        }
        else
        {
            GameManager.I.SoundManager.StartSFX("ButtonClick");
            GameManager.I.BackendManager.Login();
            GameManager.I.ScenesManager.LoadLoadingScene("LobbyScene");
        }
    }

    //public void LoginGoogleButton()
    //{
    //    GameManager.I.SoundManager.StartSFX("ButtonClick");
    //    GameManager.I.GPGSManager.GPGSLogin();

    //    if (GameManager.I.GPGSManager.IsLogin)
    //    {
    //        _loginGoogleButton.SetActive(false);
    //        _startButton.SetActive(true);
    //    }
    //}
}
