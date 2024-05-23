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
    [SerializeField] private TMP_Text _playerRankText;
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
    [SerializeField] private GameObject _InventoryImageObject;
    [SerializeField] private RawImage _InventoryImage;
    public int _characterNum;

    private GameData _gameData;
    private CharacterData _playerData;
    private CharacterData _inventorySelectData;
    private DataWrapper _dataWrapper;

    private void Start()
    {
        _gameData = GameManager.I.DataManager.GameData;
        _dataWrapper = GameManager.I.DataManager.DataWrapper;
        _playerData = GameManager.I.DataManager.PlayerData;
        _inventorySelectData = _playerData;
        _characterNum = -1;

        if (PlayerPrefs.GetInt("Tutorial") == 0)
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
        _playerTagText.text = _playerData.KoreaTag.ToString();
        _playerRankText.text = _playerData.CharacterRank.ToString();
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
        InventoryImageSetting();
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

    public void InventorySlotButton(int num)
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        _inventorySelectData = GameManager.I.DataManager.DataWrapper.CharacterDatas[num];
        _characterNum = num;

        _playerTagText.text = _inventorySelectData.KoreaTag.ToString();
        _playerRankText.text = _inventorySelectData.CharacterRank.ToString();
        _playerLevelText.text = _inventorySelectData.Level.ToString();
        _playerExpSlider.value = (float)_inventorySelectData.CurrentExp / _inventorySelectData.MaxExp;
        _playerExpPercent.text = (((float)_inventorySelectData.CurrentExp / _inventorySelectData.MaxExp) * 100).ToString("N1") + "%";
        _atkText.text = _inventorySelectData.Atk.ToString();
        _defText.text = _inventorySelectData.Def.ToString();
        _speedText.text = _inventorySelectData.Speed.ToString();
        _skillAtkText.text = _inventorySelectData.SkillAtk.ToString();
        _skillCoolTimeText.text = _inventorySelectData.SkillCoolTime.ToString();
        _dashPowerText.text = _inventorySelectData.DashImpulse.ToString();
        _dashCoolTimeText.text = _inventorySelectData.DashCoolTime.ToString();

        InventoryImageChange();
    }

    private void InventoryImageSetting()
    {
        int count = _InventoryImageObject.transform.childCount;

        for (int i = 0; i < count; i++)
        {
            _InventoryImageObject.transform.GetChild(i).gameObject.SetActive(false);
        }

        _InventoryImageObject.transform.Find(_playerData.Tag).gameObject.SetActive(true);

        if(!_playerData.IsGet) _InventoryImage.color = new Color(20 / 255f, 20 / 255f, 20 / 255f, 255 / 255f);
        else _InventoryImage.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
    }

    private void InventoryImageChange()
    {
        int count = _InventoryImageObject.transform.childCount;

        for (int i = 0; i < count; i++)
        {
            _InventoryImageObject.transform.GetChild(i).gameObject.SetActive(false);
        }

        _InventoryImageObject.transform.Find(_inventorySelectData.Tag).gameObject.SetActive(true);

        if (!_inventorySelectData.IsGet) _InventoryImage.color = new Color(20 / 255f, 20 / 255f, 20 / 255f, 255 / 255f);
        else _InventoryImage.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
    }

    public void EquipButton()
    {
        if(_dataWrapper.CharacterDatas[_characterNum].IsGet)
        {
            GameManager.I.SoundManager.StartSFX("ButtonClick");

            for (int i = 0; i < _dataWrapper.CharacterDatas.Length; i++)
            {
                _dataWrapper.CharacterDatas[i].IsEquip = false;
            }
            _dataWrapper.CharacterDatas[_characterNum].IsEquip = true;
            GameManager.I.DataManager.PlayerData = _dataWrapper.CharacterDatas[_characterNum];
        }
        else
        {
            GameManager.I.SoundManager.StartSFX("ButtonClickMiss");
        }
    }
}
