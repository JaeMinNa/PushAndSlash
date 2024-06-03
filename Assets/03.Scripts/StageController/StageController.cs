using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Cinemachine;

public class StageController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private TMP_Text _coinText;
    [SerializeField] private TMP_Text _stageText;
    [HideInInspector] public int StageCoin;

    [Header("GameClear")]
    [SerializeField] private GameObject _gameClear;
    [SerializeField] private GameObject _starEffect;
    [SerializeField] private TMP_Text _stageClearCoinText;
    [SerializeField] private TMP_Text _stageCoinBonusText;
    [SerializeField] private GameObject[] _stageClearStarObjects;
    [SerializeField] private TMP_Text _stageClearLevelText;
    [SerializeField] private TMP_Text _stageClearExpText;
    [SerializeField] private Slider _stageClearExpSlider;
    private bool _isGameClear;
    private int _stageCoinBondus;

    [Header("GameOver")]
    [SerializeField] private GameObject _gameOver;
    [SerializeField] private TMP_Text _stageGameOverCoinText;
    [SerializeField] private TMP_Text _stageGameOverLevelText;
    [SerializeField] private TMP_Text _stageGameOverExpText;
    [SerializeField] private Slider _stageGameOverExpSlider;
    private bool _isGameOver;

    [Header("StageTitle")]
    [SerializeField] private TMP_Text _stageTitleText;

    [Header("Camera")]
    [SerializeField] private GameObject _virtualCamera;
    private Camera _mainCamera;
    private CameraController _cameraController;

    [HideInInspector] public int StageExp;
    private float _time;
    private GameObject _player;
    private GameData _gameData;
    private DataManager _dataManager;
    private List<CharacterData> _inventory;

    private void Start()
    {
        _player = GameManager.I.PlayerManager.Player;
        _gameData = GameManager.I.DataManager.GameData;
        _dataManager = GameManager.I.DataManager;
        _inventory = _dataManager.DataWrapper.CharacterInventory;
        _mainCamera = Camera.main;
        _cameraController = _mainCamera.GetComponent<CameraController>();
        _time = 180f;
        _isGameClear = false;
        _isGameOver = false;
        StageCoin = 0;
        _stageCoinBondus = 0;

        StageSetting();
        StageTextSetting();
        CoinSetting();
        StartCoroutine(COStartCameraSetting());

        //Time.timeScale = 0f;
    }

    private void Update()
    {

        _time -= Time.deltaTime;
        TimeTextUpdate();

        if (!IsEnemy() && !_isGameClear)
        {
            _isGameClear = true;
            GameClear();
        }

        if ((_time <= 0 || !IsPlayer()) && !_isGameOver)
        {
            _isGameOver = true;
            GameOver();
        }
    }

    #region UI
    private void TimeTextUpdate()
    {
        string text;
        if (Mathf.Floor(_time % 60) < 10)
        {
            text = "0" + Mathf.Floor(_time % 60).ToString();
        }
        else text = Mathf.Floor(_time % 60).ToString();

        _timeText.text = Mathf.Floor(_time / 60).ToString() + ":" + text;
    }

    private void StageTextSetting()
    {
        string name = GameManager.I.ScenesManager.CurrentSceneName.Substring(11);
        _stageTitleText.text = _stageText.text = "Chapter " + name + "-" + _gameData.Stage;
        _stageText.text = "Chapter " + name + "-" + _gameData.Stage;
    }

    public void CoinSetting()
    {
        _coinText.text = StageCoin.ToString();
    }
    #endregion

    #region Game
    private void GameClear()
    {
        Time.timeScale = 0f;
        _stageClearCoinText.text = StageCoin.ToString();
        if(_time >= 120)
        {
            StarEffectSetting(3);
            _stageCoinBondus = 300;
        }
        else if(_time >= 60)
        {
            StarEffectSetting(2);
            _stageCoinBondus = 200;
        }
        else
        {
            StarEffectSetting(1);
            _stageCoinBondus = 100;
        }
        _stageCoinBonusText.text = _stageCoinBondus.ToString();
        GameManager.I.DataManager.GameData.Coin += StageCoin;
        GameManager.I.DataManager.GameData.Coin += _stageCoinBondus;

        LevelExpUp(StageExp);
        PlayerDataToInventoryData();
        _stageClearLevelText.text = _dataManager.PlayerData.Level.ToString();
        _stageClearExpText.text = _dataManager.PlayerData.CurrentExp.ToString() + "/" + _dataManager.PlayerData.MaxExp.ToString();
        _stageClearExpSlider.value = (float)_dataManager.PlayerData.CurrentExp / _dataManager.PlayerData.MaxExp;

        if(_gameData.Stage <= 9) GameManager.I.DataManager.GameData.Stage++;
        _gameClear.SetActive(true);

        GameManager.I.DataManager.DataSave();
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        _stageGameOverCoinText.text = StageCoin.ToString();
        GameManager.I.DataManager.GameData.Coin += StageCoin;
        
        LevelExpUp(StageExp);
        PlayerDataToInventoryData();
        _stageGameOverLevelText.text = _dataManager.PlayerData.Level.ToString();
        _stageGameOverExpText.text = _dataManager.PlayerData.CurrentExp.ToString() + "/" + _dataManager.PlayerData.MaxExp.ToString();
        _stageGameOverExpSlider.value = (float)_dataManager.PlayerData.CurrentExp / _dataManager.PlayerData.MaxExp;

        _gameOver.SetActive(true);

        GameManager.I.DataManager.DataSave();
    }

    private void StageSetting()
    {
        transform.GetChild(_gameData.Stage - 1).gameObject.SetActive(true);
    }

    private bool IsEnemy()
    {
        int enemyCount = transform.GetChild(_gameData.Stage - 1).childCount;

        for (int i = 0; i < enemyCount; i++)
        {
            if (transform.GetChild(_gameData.Stage - 1).GetChild(i).gameObject.activeSelf) return true;
        }

        return false;
    }

    private bool IsPlayer()
    {
        if (_player.transform.position.y <= -10f) return false;

        return true;
    }

    public void TimeScaleStart()
    {
        Time.timeScale = 1f;
    }

    public void LobbySenceButton()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        GameManager.I.ScenesManager.LoadScene("LobbySence");
    }

    private void StarEffectSetting(int num)
    {
        int count = _starEffect.transform.childCount;

        for (int i = 0; i < count; i++)
        {
            _starEffect.transform.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < 3; i++)
        {
            _stageClearStarObjects[i].SetActive(false);
        }

        if (num == 0) return;
        else if (num == 1 || num == 2)
        {
            for (int i = 0; i < num; i++)
            {
                _starEffect.transform.GetChild(i).gameObject.SetActive(true);
                _stageClearStarObjects[i].SetActive(true);
            }
        }
        else
        {
            _starEffect.transform.GetChild(2).gameObject.SetActive(true);
            for (int i = 0; i < num; i++)
            {
                _stageClearStarObjects[i].SetActive(true);
            }
        }
    }

    private void LevelExpUp(int exp)
    {
        if (_dataManager.PlayerData.Level >= 30) return;

        GameManager.I.DataManager.PlayerData.CurrentExp += exp;

        if(_dataManager.PlayerData.CurrentExp >= _dataManager.PlayerData.MaxExp)
        {
            GameManager.I.DataManager.PlayerData.Level++;
            GameManager.I.DataManager.PlayerData.CurrentExp = _dataManager.PlayerData.CurrentExp - _dataManager.PlayerData.MaxExp;
            GameManager.I.DataManager.PlayerData.MaxExp = 20 + (_dataManager.PlayerData.Level * 10);
        }
    }

    private void PlayerDataToInventoryData()
    {
        int inventoryOrder = FindInventoryOrder(_dataManager.PlayerData);

        GameManager.I.DataManager.DataWrapper.CharacterInventory[inventoryOrder].CurrentExp = _dataManager.PlayerData.CurrentExp;
        GameManager.I.DataManager.DataWrapper.CharacterInventory[inventoryOrder].MaxExp = _dataManager.PlayerData.MaxExp;
        GameManager.I.DataManager.DataWrapper.CharacterInventory[inventoryOrder].Level = _dataManager.PlayerData.Level;
    }

    private int FindInventoryOrder(CharacterData data)
    {
        int count = _inventory.Count;

        for (int i = 0; i < count; i++)
        {
            if (data.Tag == _inventory[i].Tag) return i;
            else continue;
        }

        return -1;
    }
    #endregion

    #region Camera
    IEnumerator COStartCameraSetting()
    {
        yield return new WaitForSecondsRealtime(4f);

        _virtualCamera.SetActive(false);
        _mainCamera.transform.rotation = Quaternion.Euler(_cameraController.OriginCameraRotation);
    }
    #endregion

}
