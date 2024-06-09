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
    [SerializeField] private GameObject[] _roomMyInfoObjects;
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

    private bool _isNoEnemy;
    //public string _roomEnemyCharacterName;
    //public int _roomEnemyCharacterLevel;
    //public string _roomEnemyCharacterRank;
    //public string _roomEnemyUserName;
    //public int _roomEnemyRankPoint;
    //public int _roomEnemyCharacterStar;

    //private PhotonView _photonView;

    private void Start()
    {
        _isNoEnemy = true;
    }

    private void Awake()
    {
        //_photonView = GetComponent<PhotonView>();

        Screen.SetResolution(960, 540, false);
    }

    private void Update()
    {
        if (PhotonNetwork.InRoom)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 1 && _isNoEnemy)
            {
                _isNoEnemy = false;
                NoEnemyInRoom();
            }
            else if (PhotonNetwork.CurrentRoom.PlayerCount == 2 && !_isNoEnemy)
            {
                _isNoEnemy = true;
            }
        }
    }

    #region Common
    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()  // 콜백 : 성공적으로 실행되었다면, 함수가 실행
    {
        Debug.Log("서버접속완료");
        PhotonNetwork.LocalPlayer.NickName = GameManager.I.DataManager.GameData.UserName;
        MyDataSettingInRoom();
        JoinRandomOrCreateRoom();
    }

    public void DisConnect()
    {
        PhotonNetwork.Disconnect();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("서버연결끊김");
    }

    public void JoinLobby()
    {
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("로비접속완료");
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
        PhotonNetwork.JoinRandomOrCreateRoom(expectedMaxPlayers : 2, roomOptions : new RoomOptions() { MaxPlayers = 2 });
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("방만들기완료");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("방참가완료");
        PhotonNetwork.Instantiate("PUN2/Room/RoomController", transform.position, Quaternion.identity);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("방만들기실패");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("방참가실패");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("방랜덤참가실패");
    }

    [ContextMenu("Multi Info")]
    public void Info()
    {
        if(PhotonNetwork.InRoom) // 방에 있다면
        {
            Debug.Log("현재 방 이름 : " + PhotonNetwork.CurrentRoom.Name);
            Debug.Log("현재 방 인원수 : " + PhotonNetwork.CurrentRoom.PlayerCount);
            Debug.Log("현재 방 최대 인원수 : " + PhotonNetwork.CurrentRoom.MaxPlayers);

            string playerStr = "방에 있는 플레이어 목록 : ";
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                playerStr += PhotonNetwork.PlayerList[i].NickName + ", ";
            }
            Debug.Log(playerStr);
        }
        else
        {
            Debug.Log("접속한 인원수 : " + PhotonNetwork.CountOfPlayers);
            Debug.Log("방 개수 : " + PhotonNetwork.CountOfRooms);
            Debug.Log("모든 방에 있는 인원수 : " + PhotonNetwork.CountOfPlayersInRooms);
            Debug.Log("로비에 있는지? : " + PhotonNetwork.InLobby);
            Debug.Log("연결됐는지? : " + PhotonNetwork.IsConnected);
        }
    }
    #endregion

    #region Room
    public void MultiPlayActive()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");

        for (int i = 0; i < _roomMyInfoObjects.Length; i++)
        {
            _roomMyInfoObjects[i].SetActive(false);
        }

        for (int i = 0; i < _roomEnemyInfoObjects.Length; i++)
        {
            _roomEnemyInfoObjects[i].SetActive(false);
        }

        _multiPlayPanel.SetActive(true);
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

        for (int i = 0; i < _roomEnemyInfoObjects.Length; i++)
        {
            _roomMyInfoObjects[i].SetActive(true);
        }
    }

    public void EnemyDataSettingInRoom(string characterName, int level, string rank, string userName, int rankPoint, int star, string tag)
    {
        //for (int i = 0; i < _roomEnemyInfoObjects.Length; i++)
        //{
        //    _roomEnemyInfoObjects[i].SetActive(true);
        //}

        _roomEnemyCharacterNameText.text = characterName;
        _roomEnemyCharacterLevelText.text = "Lv " + level.ToString();
        _roomEnemyCharacterRankText.text = rank;
        _roomEnemyUserNameText.text = userName;
        _roomEnemyRankPointText.text = rankPoint.ToString();
        MultiPlayEnemyImageSetting(tag);
        ActiveEnemyStar(star);

        for (int i = 0; i < _roomEnemyInfoObjects.Length; i++)
        {
            _roomEnemyInfoObjects[i].SetActive(true);
        }
    }

    private void MultiPlayMyImageSetting()
    {
        int count = _roomMyCharacterImageObject.transform.childCount;

        for (int i = 0; i < count; i++)
        {
            _roomMyCharacterImageObject.transform.GetChild(i).gameObject.SetActive(false);
        }

        _roomMyCharacterImageObject.transform.Find(GameManager.I.DataManager.PlayerData.Tag).gameObject.SetActive(true);
        _roomMyCharacterImageObject.transform.Find("LenderCamera").gameObject.SetActive(true);
    }

    private void MultiPlayEnemyImageSetting(string tag)
    {
        int count = _roomEnemyCharacterImageObject.transform.childCount;

        for (int i = 0; i < count; i++)
        {
            _roomEnemyCharacterImageObject.transform.GetChild(i).gameObject.SetActive(false);
        }

        _roomEnemyCharacterImageObject.transform.Find(tag).gameObject.SetActive(true);
        _roomEnemyCharacterImageObject.transform.Find("LenderCamera").gameObject.SetActive(true);
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

    private void ActiveEnemyStar(int starNum)
    {
        for (int i = 0; i < 5; i++)
        {
            _roomEnemyCharacterUpgradeStars[i].SetActive(false);
        }

        if (starNum == 0) return;
        else _roomEnemyCharacterUpgradeStars[starNum - 1].SetActive(true);
    }

    private void NoEnemyInRoom()
    {
        for (int i = 0; i < _roomEnemyInfoObjects.Length; i++)
        {
            _roomEnemyInfoObjects[i].SetActive(false);
        }
    }

    //[PunRPC]
    //private void JoinRoom(CharacterData playerData, GameData gameData)
    //{
    //    _roomEnemyCharacterNameText.text = playerData.KoreaTag;
    //    _roomEnemyCharacterLevelText.text = "Lv " + playerData.Level.ToString();
    //    _roomEnemyCharacterRankText.text = playerData.CharacterRank.ToString();
    //    _roomEnemyUserNameText.text = PhotonNetwork.LocalPlayer.NickName;
    //    _roomEnemyRankPointText.text = gameData.RankPoint.ToString();
    //    MultiPlayEnemyImageSetting(playerData);
    //    ActiveEnemyStar(playerData.Star);

    //    EnemyDataSettingInRoom();

    //public string _roomEnemyCharacterName;
    //public int _roomEnemyCharacterLevel;
    //public string _roomEnemyCharacterRank;
    //public string _roomEnemyUserName;
    //public int _roomEnemyRankPoint;
    //}

    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if(stream.IsWriting)
    //    {
    //        stream.SendNext(GameManager.I.DataManager.PlayerData.KoreaTag);
    //        stream.SendNext(GameManager.I.DataManager.PlayerData.Level);
    //        stream.SendNext(GameManager.I.DataManager.PlayerData.CharacterRank);
    //        stream.SendNext(GameManager.I.DataManager.GameData.UserName);
    //        stream.SendNext(GameManager.I.DataManager.GameData.RankPoint);
    //        stream.SendNext(GameManager.I.DataManager.PlayerData.Star);
    //    }
    //    else
    //    {
    //        _roomEnemyCharacterName = (string)stream.ReceiveNext();
    //        _roomEnemyCharacterLevel = (int)stream.ReceiveNext();
    //        _roomEnemyCharacterRank = (string)stream.ReceiveNext();
    //        _roomEnemyUserName = (string)stream.ReceiveNext();
    //        _roomEnemyRankPoint = (int)stream.ReceiveNext();
    //        _roomEnemyCharacterStar = (int)stream.ReceiveNext();
    //    }
    //}
    #endregion
}
