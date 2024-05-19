using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private TMP_Text _coinText;
    [SerializeField] private TMP_Text _stageText;

    [Header("GameClear")]
    [SerializeField] private GameObject _gameClear;

    [Header("GameOver")]
    [SerializeField] private GameObject _gameOver;

    [Header("StageTitle")]
    [SerializeField] private TMP_Text _stageTitleText;

    private float _time;
    private GameObject _player;
    private GameData _gameData;
    private bool _isGameClear;
    private bool _isGameOver;

    private void Start()
    {
        _player = GameManager.I.PlayerManager.Player;
        _gameData = GameManager.I.DataManager.GameData;
        _time = 180f;
        _isGameClear = false;
        _isGameOver = false;

        StageSetting();
        StageTextSetting();
        CoinSetting();

        Time.timeScale = 0f;
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

    private void GameClear()
    {
        Time.timeScale = 0f;
        _gameClear.SetActive(true);
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        _gameOver.SetActive(true);
    }

    private void StageSetting()
    {
        transform.GetChild(_gameData.Stage - 1).gameObject.SetActive(true);
    }

    private void StageTextSetting()
    {
        string name = GameManager.I.ScenesManager.CurrentSceneName.Substring(11);
        _stageTitleText.text = _stageText.text = "Chapter " + name + "-" + _gameData.Stage;
        _stageText.text = "Chapter " + name + "-" + _gameData.Stage;
    }

    private void CoinSetting()
    {
        _coinText.text = _gameData.Coin.ToString();
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
}
