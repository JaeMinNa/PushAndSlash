using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text _coinText;
    [SerializeField] private TMP_Text _userNameText;
    [SerializeField] private Slider _expSlider;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _expText;

    private GameData _gameData;
    private CharacterData _playerData;

    private void Start()
    {
        _gameData = GameManager.I.DataManager.GameData;
        _playerData = GameManager.I.DataManager.PlayerData;

        CoinSetting();
        UserNameSetting();
        CharacterSetting();
    }

    private void CoinSetting()
    {
        _coinText.text = _gameData.Coin.ToString();
    }

    private void UserNameSetting()
    {
        _userNameText.text = _gameData.UserName;
    }

    private void LevelSetting()
    {
        _levelText.text = _playerData.Level.ToString();
    }

    private void ExpSetting()
    {
        _expSlider.value = (float)_playerData.CurrentExp / _playerData.MaxExp;
        _expText.text = _playerData.CurrentExp.ToString() + "/" + _playerData.MaxExp.ToString();
    }

    private void CharacterSetting()
    {
        LevelSetting();
        ExpSetting();
    }
}
