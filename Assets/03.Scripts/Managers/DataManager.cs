using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public string UserName = "";
    public int Stage = 1;
    public int Coin = 0;
}

[System.Serializable]
public class DataWrapper
{
    public CharacterData[] CharacterDatas;
    public List<CharacterData> CharacterInventory;
}

[System.Serializable]
public class CharacterData
{
    [Header("Common Stats")]
    public string Tag;
    //public bool IsGet;
    //public bool IsEquip;
    public int Level;
    public float Speed;
    public float DashImpulse;
    public float DashCoolTime;
    public float Atk;
    public float Def;
    public float SkillAtk;
    public float SkillCoolTime;
}

public class DataManager : MonoBehaviour
{
    public CharacterData PlayerData;
    public GameData GameData;
    public DataWrapper DataWrapper;

    public void Init()
    {
        DataLoad();
    }

    public void Release()
    {

    }

    public void DataSave()
    {
        ES3.Save("GameData", GameData); // Key값 설정, 선언한 class 변수명

        //ES3AutoSaveMgr.Current.Save();
    }

    public void DataLoad()
    {
        if(ES3.FileExists("SaveFile.txt"))
        {
            ES3.LoadInto("GameData", GameData); // 저장된 Key 값, 불러올 class 변수명

            //ES3AutoSaveMgr.Current.Load();
        }
        else
        {
            DataSave();
        }
    }
}
