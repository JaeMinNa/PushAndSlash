using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomController : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] private bool _myIsReady;
    [SerializeField] private bool _enemyIsReady;
    private PhotonView _photonView;
    private NetworkManager _networkManager;
    private bool _isJoin;
    private bool _isGameStart;
    private bool _isReady;
    private string _roomEnemyCharacterName;
    private int _roomEnemyCharacterLevel;
    private string _roomEnemyCharacterRank;
    private string _roomEnemyUserName;
    private int _roomEnemyRankPoint;
    private int _roomEnemyCharacterStar;
    private string _roomEnemyCharacterKorTag;


    private void Awake()
    {
        _networkManager = GameObject.FindWithTag("NetworkManager").GetComponent<NetworkManager>();
        _photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        _isJoin = false;
        _isGameStart = false;
        _isReady = false;
        _myIsReady = false;
        _enemyIsReady = false;
        StartCoroutine(COUpdate());
    }

    private void Update()
    {
        _myIsReady = _networkManager.IsReady;

        if (!_photonView.IsMine)
        {
            if (_enemyIsReady && !_isReady)
            {
                _isReady = true;
                _networkManager.EnemyReadyActive(true);
            }
            else if(!_enemyIsReady && _isReady)
            {
                _isReady = false;
                _networkManager.EnemyReadyActive(false);
            }

            if (_myIsReady && _enemyIsReady && !_isGameStart)
            {
                _isGameStart = true;
                Debug.Log("멀티 대전 시작!");
            }
        }
    }


    IEnumerator COUpdate()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            if (!_photonView.IsMine)
            {
                if (PhotonNetwork.InRoom && !_isJoin)
                {
                    _isJoin = true;

                    if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
                        _networkManager.EnemyDataSettingInRoom(_roomEnemyCharacterName, _roomEnemyCharacterLevel, _roomEnemyCharacterRank.ToString(), _roomEnemyUserName, _roomEnemyRankPoint, _roomEnemyCharacterStar, _roomEnemyCharacterKorTag);
                }
            }

            yield return null;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(GameManager.I.DataManager.PlayerData.KoreaTag);
            stream.SendNext(GameManager.I.DataManager.PlayerData.Level);
            stream.SendNext(GameManager.I.DataManager.PlayerData.CharacterRank.ToString());
            stream.SendNext(GameManager.I.DataManager.GameData.UserName);
            stream.SendNext(GameManager.I.DataManager.GameData.RankPoint);
            stream.SendNext(GameManager.I.DataManager.PlayerData.Star);
            stream.SendNext(GameManager.I.DataManager.PlayerData.Tag);
            stream.SendNext(_myIsReady);
        }
        else
        {
            _roomEnemyCharacterName = (string)stream.ReceiveNext();
            _roomEnemyCharacterLevel = (int)stream.ReceiveNext();
            _roomEnemyCharacterRank = (string)stream.ReceiveNext();
            _roomEnemyUserName = (string)stream.ReceiveNext();
            _roomEnemyRankPoint = (int)stream.ReceiveNext();
            _roomEnemyCharacterStar = (int)stream.ReceiveNext();
            _roomEnemyCharacterKorTag = (string)stream.ReceiveNext();
            _enemyIsReady = (bool)stream.ReceiveNext();
        }
    }
}
