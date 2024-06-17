using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd; // �ڳ� SDK namespace �߰�

public class BackendManager : MonoBehaviour
{
    

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
}