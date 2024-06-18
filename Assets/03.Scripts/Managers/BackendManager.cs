using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd; // �ڳ� SDK namespace �߰�

public class BackendManager : MonoBehaviour
{
    public class BackendGameData
    {
        private static BackendGameData instance = null;
        public static BackendGameData Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new BackendGameData();
                }

                return instance;
            }
        }

        private GameData gameData = new GameData();
        public GameData GameData => gameData;

        private string gameDataRowInDate = string.Empty;

        // �ڳ� �ܼ� ���̺� ���ο� ���� ���� �߰�
        public void GameDataInsert()
        {
            // ���� ������ �ʱⰪ���� ����
            
        }
    }

    

    public void Init()
    {
        BackendSetup();
    }

    public void Release()
    {

    }

    private void BackendSetup()
    {
        var bro = Backend.Initialize(true); // �ڳ� �ʱ�ȭ

        // �ڳ� �ʱ�ȭ�� ���� ���䰪
        if (bro.IsSuccess())
        {
            Debug.Log("�ʱ�ȭ ���� : " + bro); // ������ ��� statusCode 204 Success
        }
        else
        {
            Debug.LogError("�ʱ�ȭ ���� : " + bro); // ������ ��� statusCode 400�� ���� �߻�
        }
    }

    public void GetUserID()
    {

    }

    public void SignUp()
    {
        BackendReturnObject bro = Backend.BMember.CustomSignUp(GameManager.I.GPGSManager.GetGPGSUserID(), "1234");
        if (bro.IsSuccess())
        {
            Debug.Log("ȸ�����Կ� �����߽��ϴ�");
        }
    }

    public void Login()
    {
        BackendReturnObject bro = Backend.BMember.CustomLogin(GameManager.I.GPGSManager.GetGPGSUserID(), "1234");
        if (bro.IsSuccess())
        {
            Debug.Log("�α��ο� �����߽��ϴ�");
        }
        else
        {
            Debug.Log("�α��ο� �����߽��ϴ�");
            Debug.Log("ȸ�������� �����մϴ�");
            SignUp();
        }
    }
}