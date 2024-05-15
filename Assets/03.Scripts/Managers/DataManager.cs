using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public string UserName;
    public int Stage;
    public int Coin;
    public float SFXValume;
    public float BGMValume;
}

[System.Serializable]
public class DataWrapper
{
    public List<CharacterData> CharacterInventory;
    public CharacterData[] CharacterDatas;
    public EnemyData[] EnemyDatas;
}

[System.Serializable]
public class CharacterData
{
    public enum Rank
    {
        C,
        B,
        A,
        S,
        SS,
        SSS,
    }

    [Header("Common Stats")]
    public string Tag;
    //public bool IsGet;
    //public bool IsEquip;
    public Rank CharacterRank;
    public int Level;
    public int CurrentExp;
    public int MaxExp;
    public float Speed;
    public float DashImpulse;
    public float DashCoolTime;
    public float Atk;
    public float Def;
    public float SkillAtk;
    public float SkillCoolTime;
}

[System.Serializable]
public class EnemyData
{
    [Header("Common Stats")]
    public string Tag;
    public int Exp;
    public float Speed;
    public float Atk;
    public float AttackCoolTime;
    public float Def;
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
        //ES3.Save("GameData", GameData); // Key�� ����, ������ class ������
        //ES3.Save("PlayerData", PlayerData);
        //ES3.Save("DataWrapper", DataWrapper);

        ES3AutoSaveMgr.Current.Save();
    }

    public void DataLoad()
    {
        if(ES3.FileExists("SaveFile.txt"))
        {
            //ES3.LoadInto("GameData", GameData); // ����� Key ��, �ҷ��� class ������
            //ES3.LoadInto("PlayerData", PlayerData);
            //ES3.LoadInto("DataWrapper", DataWrapper);

            ES3AutoSaveMgr.Current.Load();
        }
        else
        {
            DataSave();
        }
    }

    public void DataDelete()
    {
        ES3.DeleteFile("SaveFile.txt");

        // PlayerPrefs �ʱ�ȭ
        PlayerPrefs.DeleteAll();

        // GameData
        GameData.UserName = "UserName";
        GameData.Stage = 1;
        GameData.Coin = 0;
        GameData.SFXValume = 0;
        GameData.BGMValume = 0;

        // Inventory
        DataWrapper.CharacterInventory.Clear();
    }
}
