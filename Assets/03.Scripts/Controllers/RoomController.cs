using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class RoomController : MonoBehaviourPunCallbacks, IPunObservable
{
    //[SerializeField] private TMP_Text _roomEnemyCharacterNameText;
    //[SerializeField] private TMP_Text _roomEnemyCharacterLevelText;
    //[SerializeField] private TMP_Text _roomEnemyCharacterRankText;
    //[SerializeField] private TMP_Text _roomEnemyUserNameText;
    //[SerializeField] private TMP_Text _roomEnemyRankPointText;
    //[SerializeField] private GameObject _roomEnemyCharacterImageObject;
    //[SerializeField] private GameObject[] _roomEnemyCharacterUpgradeStars;
    private PhotonView _photonView;
    private NetworkManager _networkManager;
    public bool _isJoin;
    public string _roomEnemyCharacterName;
    public int _roomEnemyCharacterLevel;
    public string _roomEnemyCharacterRank;
    public string _roomEnemyUserName;
    public int _roomEnemyRankPoint;
    public int _roomEnemyCharacterStar;
    public string _roomEnemyCharacterKorTag;


    private void Awake()
    {
        _networkManager = GameObject.FindWithTag("NetworkManager").GetComponent<NetworkManager>();
        _photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        _isJoin = false;
        StartCoroutine(COUpdate());
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

            yield return null; // 다음 프레임까지 대기
        }
    }

    //private void EnemyDataSettingInRoom()
    //{
    //    _roomEnemyCharacterNameText.text = _roomEnemyCharacterName;
    //    _roomEnemyCharacterLevelText.text = "Lv " + _roomEnemyCharacterLevel.ToString();
    //    _roomEnemyCharacterRankText.text = _roomEnemyCharacterRank;
    //    _roomEnemyUserNameText.text = _roomEnemyUserName;
    //    _roomEnemyRankPointText.text = _roomEnemyRankPoint.ToString();
    //    MultiPlayEnemyImageSetting(_roomEnemyCharacterName);
    //    ActiveEnemyStar(_roomEnemyCharacterStar);

    //    for (int i = 0; i < _roomEnemyInfoObjects.Length; i++)
    //    {
    //        _roomEnemyInfoObjects[i].SetActive(true);
    //    }
    //}

    //private void ActiveEnemyStar(int starNum)
    //{
    //    for (int i = 0; i < 5; i++)
    //    {
    //        _roomEnemyCharacterUpgradeStars[i].SetActive(false);
    //    }

    //    if (starNum == 0) return;
    //    else _roomEnemyCharacterUpgradeStars[starNum - 1].SetActive(true);
    //}

    //private void MultiPlayEnemyImageSetting(string tag)
    //{
    //    int count = _roomEnemyCharacterImageObject.transform.childCount;

    //    for (int i = 0; i < count; i++)
    //    {
    //        _roomEnemyCharacterImageObject.transform.GetChild(i).gameObject.SetActive(false);
    //    }

    //    _roomEnemyCharacterImageObject.transform.Find(tag).gameObject.SetActive(true);
    //}

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
        }
    }
}
