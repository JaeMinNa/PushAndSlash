using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GPGSManager : MonoBehaviour
{
    public void Init()
    {
        if (GameManager.I.ScenesManager.CurrentSceneName == "StartScene")
        {
            GPGSLogin();
        }
    }

    public void Release()
    {

    }

    public string GetGPGSUserDisplayName()
    {
        return PlayGamesPlatform.Instance.GetUserDisplayName();
    }

    public string GetGPGSUserID()
    {
        return PlayGamesPlatform.Instance.GetUserId();
    }

    public void GPGSLogin()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            string displayName = PlayGamesPlatform.Instance.GetUserDisplayName();   // 유저가 변경 가능
            string userID = PlayGamesPlatform.Instance.GetUserId(); // 유저가 변경 불가능

            Debug.Log("로그인 성공, " + "DisplayName : " + displayName + ", UserID : " + userID);
        }
        else
        {
            Debug.Log("로그인 실패");
        }
    }

}
