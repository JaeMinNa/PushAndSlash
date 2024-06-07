using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _multiPlayPanel;

    [Header("RoomMyCharacterInfo")]
    [SerializeField] private TMP_Text _roomMyCharacterNameText;
    [SerializeField] private TMP_Text _roomMyCharacterLevelText;
    [SerializeField] private TMP_Text _roomMyCharacterRankText;
    [SerializeField] private TMP_Text _roomMyUserNameText;
    [SerializeField] private TMP_Text _roomMyRankPointText;
    [SerializeField] private GameObject _roomMyCharacterImageObject;
    [SerializeField] private GameObject[] _roomMyCharacterUpgradeStars;

    [Header("RoomEnemyCharacterInfo")]
    [SerializeField] private GameObject[] _roomEnemyInfoObjects;
    [SerializeField] private TMP_Text _roomEnemyCharacterNameText;
    [SerializeField] private TMP_Text _roomEnemyCharacterLevelText;
    [SerializeField] private TMP_Text _roomEnemyCharacterRankText;
    [SerializeField] private TMP_Text _roomEnemyUserNameText;
    [SerializeField] private TMP_Text _roomEnemyRankPointText;
    [SerializeField] private GameObject _roomEnemyCharacterImageObject;
    [SerializeField] private GameObject[] _roomEnemyCharacterUpgradeStars;

    private void Awake()
    {
        Screen.SetResolution(960, 540, false);
    }

    #region Common
    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()  // �ݹ� : ���������� ����Ǿ��ٸ�, �Լ��� ����
    {
        Debug.Log("�������ӿϷ�");
        PhotonNetwork.LocalPlayer.NickName = GameManager.I.DataManager.GameData.UserName;

        JoinRandomOrCreateRoom();
    }

    public void DisConnect()
    {
        PhotonNetwork.Disconnect();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("�����������");
    }

    public void JoinLobby()
    {
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("�κ����ӿϷ�");
    }

    public void CreateRoom()
    {
        string roomName = "Room";
        PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = 2 });
    }

    public void JoinRoom()
    {
        string roomName = "Room";
        PhotonNetwork.JoinRoom(roomName);
    }

    public void JoinOrCreateRoom()
    {
        string roomName = "Room";
        PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions { MaxPlayers = 2 }, null);
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void JoinRandomOrCreateRoom()
    {
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("�游���Ϸ�");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("�������Ϸ�");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("�游������");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("����������");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("�淣����������");
    }

    [ContextMenu("Multi Info")]
    public void Info()
    {
        if(PhotonNetwork.InRoom) // �濡 �ִٸ�
        {
            Debug.Log("���� �� �̸� : " + PhotonNetwork.CurrentRoom.Name);
            Debug.Log("���� �� �ο��� : " + PhotonNetwork.CurrentRoom.PlayerCount);
            Debug.Log("���� �� �ִ� �ο��� : " + PhotonNetwork.CurrentRoom.MaxPlayers);

            string playerStr = "�濡 �ִ� �÷��̾� ��� : ";
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                playerStr += PhotonNetwork.PlayerList[i].NickName + ", ";
            }
            Debug.Log(playerStr);
        }
        else
        {
            Debug.Log("������ �ο��� : " + PhotonNetwork.CountOfPlayers);
            Debug.Log("�� ���� : " + PhotonNetwork.CountOfRooms);
            Debug.Log("��� �濡 �ִ� �ο��� : " + PhotonNetwork.CountOfPlayersInRooms);
            Debug.Log("�κ� �ִ���? : " + PhotonNetwork.InLobby);
            Debug.Log("����ƴ���? : " + PhotonNetwork.IsConnected);
        }
    }
    #endregion

    #region Room
    public void MultiPlayActive()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        _multiPlayPanel.SetActive(true);
        MyDataSettingInRoom();
        Connect();
    }

    public void MultiPlayInactive()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        _multiPlayPanel.SetActive(false);
        DisConnect();
    }

    private void MyDataSettingInRoom()
    {
        _roomMyCharacterNameText.text = GameManager.I.DataManager.PlayerData.KoreaTag;
        _roomMyCharacterLevelText.text = "Lv " + GameManager.I.DataManager.PlayerData.Level.ToString();
        _roomMyCharacterRankText.text = GameManager.I.DataManager.PlayerData.CharacterRank.ToString();
        _roomMyUserNameText.text = PhotonNetwork.LocalPlayer.NickName;
        _roomMyRankPointText.text = GameManager.I.DataManager.GameData.RankPoint.ToString();
        MultiPlayMyImageSetting();
        ActiveMyStar(GameManager.I.DataManager.PlayerData.Star);
    }

    private void MultiPlayMyImageSetting()
    {
        int count = _roomMyCharacterImageObject.transform.childCount;

        for (int i = 0; i < count; i++)
        {
            _roomMyCharacterImageObject.transform.GetChild(i).gameObject.SetActive(false);
        }

        _roomMyCharacterImageObject.transform.Find(GameManager.I.DataManager.PlayerData.Tag).gameObject.SetActive(true);
    }

    private void ActiveMyStar(int starNum)
    {
        for (int i = 0; i < 5; i++)
        {
            _roomMyCharacterUpgradeStars[i].SetActive(false);
        }

        if (starNum == 0) return;
        else _roomMyCharacterUpgradeStars[starNum - 1].SetActive(true);
    }

    private void EnemyDataSettingInRoom()
    {

    }
    #endregion
}
