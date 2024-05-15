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
    [SerializeField] private TMP_Text _stageText_Chapter1;

    [Header("Inventory")]
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private TMP_Text _playerTagText;
    [SerializeField] private TMP_Text _playerLevelText;
    [SerializeField] private Slider _playerExpSlider;
    [SerializeField] private TMP_Text _playerExpPercent;
    [SerializeField] private TMP_Text _atkText;
    [SerializeField] private TMP_Text _defText;
    [SerializeField] private TMP_Text _speedText;
    [SerializeField] private TMP_Text _skillAtkText;
    [SerializeField] private TMP_Text _skillCoolTimeText;
    [SerializeField] private TMP_Text _dashPowerText;
    [SerializeField] private TMP_Text _dashCoolTimeText;

    private GameData _gameData;
    private CharacterData _playerData;

    private void Start()
    {
        _gameData = GameManager.I.DataManager.GameData;
        _playerData = GameManager.I.DataManager.PlayerData;

        if(PlayerPrefs.GetInt("Tutorial") == 0)
        {
            PlayerPrefs.SetInt("Tutorial", -1);
            GameManager.I.UIManager.UserNameSettingActive();
        }

        CoinSetting();
        UserNameSetting();
        StageSetting();
        CharacterSetting();
    }

    public void Chapter1Button()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        GameManager.I.DataManager.DataSave();
        GameManager.I.ScenesManager.LoadScene("BattleSence1");
    }

    public void ButtonClickMiss()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClickMiss");
    }

    public void InventoryActive()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        InventorySetting();
        _inventoryPanel.SetActive(true);
    }

    public void InventoryInactive()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        _inventoryPanel.SetActive(false);
    }

    private void InventorySetting()
    {
        _playerTagText.text = _playerData.Tag.ToString();
        _playerLevelText.text = _playerData.Level.ToString();
        _playerExpSlider.value = (float)_playerData.CurrentExp / _playerData.MaxExp;
        _playerExpPercent.text = (((float)_playerData.CurrentExp / _playerData.MaxExp) * 100).ToString("N1") + "%";
        _atkText.text = _playerData.Atk.ToString();
        _defText.text = _playerData.Def.ToString();
        _speedText.text = _playerData.Speed.ToString();
        _skillAtkText.text = _playerData.SkillAtk.ToString();
        _skillCoolTimeText.text = _playerData.SkillCoolTime.ToString();
        _dashPowerText.text = _playerData.DashImpulse.ToString();
        _dashCoolTimeText.text = _playerData.DashCoolTime.ToString();
    }

    private void CoinSetting()
    {
        _coinText.text = _gameData.Coin.ToString();
    }

    public void UserNameSetting()
    {
        _userNameText.text = _gameData.UserName;
    }

    private void StageSetting()
    {
        _stageText_Chapter1.text = "Stage " + _gameData.Stage.ToString();
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
