using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd; // 뒤끝 SDK namespace 추가

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

        // 뒤끝 콘솔 테이블에 새로운 유저 정보 추가
        public void GameDataInsert()
        {
            // 유저 정보를 초기값으로 설정
            
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
        var bro = Backend.Initialize(true); // 뒤끝 초기화

        // 뒤끝 초기화에 대한 응답값
        if (bro.IsSuccess())
        {
            Debug.Log("초기화 성공 : " + bro); // 성공일 경우 statusCode 204 Success
        }
        else
        {
            Debug.LogError("초기화 실패 : " + bro); // 실패일 경우 statusCode 400대 에러 발생
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
            Debug.Log("회원가입에 성공했습니다");
        }
    }

    public void Login()
    {
        BackendReturnObject bro = Backend.BMember.CustomLogin(GameManager.I.GPGSManager.GetGPGSUserID(), "1234");
        if (bro.IsSuccess())
        {
            Debug.Log("로그인에 성공했습니다");
        }
        else
        {
            Debug.Log("로그인에 실패했습니다");
            Debug.Log("회원가입을 진행합니다");
            SignUp();
        }
    }
}