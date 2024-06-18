using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GPGSManager : MonoBehaviour
{
    public bool IsLogin;

    public void Init()
    {
        IsLogin = false;
    }

    public void Release()
    {

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
            string displayName = PlayGamesPlatform.Instance.GetUserDisplayName();   // ������ ���� ����
            string userID = PlayGamesPlatform.Instance.GetUserId(); // ������ ���� �Ұ���

            Debug.Log("�α��� ����, " + "DisplayName : " + displayName + ", UserID : " + userID);
            IsLogin = true;
        }
        else
        {
            Debug.Log("�α��� ����");
            IsLogin = false;
        }
    }

}
