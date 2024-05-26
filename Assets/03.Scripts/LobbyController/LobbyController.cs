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
    private int _characterNum;
    private List<CharacterData> _inventory;

    [Header("CharacterSelect")]
    [SerializeField] private GameObject _CharacterSelectPanel;
    [SerializeField] private GameObject _CharacterSelectOKPanel;
    private int _charactetSelectNum;
    private CharacterData _inventorySelectData;

    [Header("Shop")]
    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private TMP_Text _shopCoinText;
    [SerializeField] private GameObject _heroPanel;
    [SerializeField] private GameObject _heroesPanel;
    [SerializeField] private TMP_Text _drawCharacterText;
    [SerializeField] private Sprite[] _frameImages;
    [SerializeField] private Image _heroPanelSlotImage;
    [SerializeField] private Image _heroPanelFrameImage;
    [SerializeField] private RawImage _heroPanelSlotRawImage;
    private CharacterData _drawCharacter;

    private GameData _gameData;
    private CharacterData _playerData;
    private DataWrapper _dataWrapper;

    private void Start()
    {
        _gameData = GameManager.I.DataManager.GameData;
        _dataWrapper = GameManager.I.DataManager.DataWrapper;
        _playerData = GameManager.I.DataManager.PlayerData;
        _inventory = _dataWrapper.CharacterInventory;
        _inventorySelectData = _playerData;
        _characterNum = -1;
        _charactetSelectNum = -1;

        if (PlayerPrefs.GetInt("Tutorial") == 0)
        {
            PlayerPrefs.SetInt("Tutorial", -1);
            _CharacterSelectPanel.SetActive(true);
            GameManager.I.UIManager.UserNameSettingActive();
        }

        CoinSetting();
        UserNameSetting();
        StageSetting();
        CharacterSetting();
    }

    public void ButtonClickMiss()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClickMiss");
    }

    #region Loby
    public void Chapter1Button()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        GameManager.I.DataManager.DataSave();
        GameManager.I.ScenesManager.LoadScene("BattleSence1");
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
    #endregion

    #region Inventory
    public void InventoryActive()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        _inventory = _dataWrapper.CharacterInventory;
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

        _InventoryImageObject.transform.Find(GameManager.I.DataManager.PlayerData.Tag).gameObject.SetActive(true);

        //if(_inventory.Contains(_playerData)) _InventoryImage.color = new Color(20 / 255f, 20 / 255f, 20 / 255f, 255 / 255f);
        //else _InventoryImage.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        _InventoryImage.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
    }

    private void InventoryImageChange()
    {
        int count = _InventoryImageObject.transform.childCount;

        for (int i = 0; i < count; i++)
        {
            _InventoryImageObject.transform.GetChild(i).gameObject.SetActive(false);
        }

        _InventoryImageObject.transform.Find(_inventorySelectData.Tag).gameObject.SetActive(true);

        if (!CharacterIsGet(_inventorySelectData)) _InventoryImage.color = new Color(20 / 255f, 20 / 255f, 20 / 255f, 255 / 255f);
        else _InventoryImage.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
    }

    public void EquipButton()
    {
        if (_characterNum == -1)
        {
            GameManager.I.SoundManager.StartSFX("ButtonClickMiss");
        }
        else if (CharacterIsGet(_inventorySelectData))
        {
            GameManager.I.SoundManager.StartSFX("EquipButton");

            for (int i = 0; i < _dataWrapper.CharacterInventory.Count; i++)
            {
                if (_inventorySelectData.Tag == _inventory[i].Tag)
                {
                    _dataWrapper.CharacterInventory[i].IsEquip = true;
                    GameManager.I.DataManager.PlayerData = _dataWrapper.CharacterInventory[i];
                }
                else _dataWrapper.CharacterInventory[i].IsEquip = false;
            }
        }
        else
        {
            GameManager.I.SoundManager.StartSFX("ButtonClickMiss");
        }

        GameManager.I.DataManager.DataSave();
    }

    private bool CharacterIsGet(CharacterData data)
    {
        for (int i = 0; i < _inventory.Count; i++)
        {
            if (data.Tag == _inventory[i].Tag) return true;
        }

        return false;
    }

    public void CaracterSelectButton(int num)
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        _charactetSelectNum = num;
        _CharacterSelectOKPanel.SetActive(true);
    }
    #endregion

    #region CharacterSelect
    public void CharacterSelectCancleButton()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        _CharacterSelectOKPanel.SetActive(false);
    }

    public void CharacterSelectConfirmedButton()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");

        if(_charactetSelectNum == 0)
        {
            GameManager.I.DataManager.DataWrapper.CharacterInventory.Add(_dataWrapper.CharacterDatas[0]);
            GameManager.I.DataManager.DataWrapper.CharacterInventory[0].IsEquip = true;
            GameManager.I.DataManager.PlayerData = _inventory[0];
        }
        else if (_charactetSelectNum == 1)
        {
            GameManager.I.DataManager.DataWrapper.CharacterInventory.Add(_dataWrapper.CharacterDatas[3]);
            GameManager.I.DataManager.DataWrapper.CharacterInventory[0].IsEquip = true;
            GameManager.I.DataManager.PlayerData = _inventory[0];
        }
        else if (_charactetSelectNum == 2)
        {
            GameManager.I.DataManager.DataWrapper.CharacterInventory.Add(_dataWrapper.CharacterDatas[1]);
            GameManager.I.DataManager.DataWrapper.CharacterInventory[0].IsEquip = true;
            GameManager.I.DataManager.PlayerData = _inventory[0];
        }

        _CharacterSelectPanel.SetActive(false);
        GameManager.I.DataManager.DataSave();
    }
    #endregion

    #region Shop
    public void ShopActive()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        _shopCoinText.text = _gameData.Coin.ToString();
        _shopPanel.SetActive(true);
    }

    public void ShopInactive()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        _shopPanel.SetActive(false);
    }

    public void DrawCharacter()
    {
        int count = _dataWrapper.CharacterDatas.Length;

        while (true)
        {
            int CharacterRank = Random.Range(0, 4);
            int CharacterNum = Random.Range(0, count);
            int randomValue = Random.Range(1, 101);

            if(CharacterRank == 0)  // B·©Å©
            {
                if (_dataWrapper.CharacterDatas[CharacterNum].CharacterRank != CharacterData.Rank.B) continue;
                else
                {
                    if (randomValue <= 70)  // B·©Å© È®·ü
                    {
                        _drawCharacter = _dataWrapper.CharacterDatas[CharacterNum];

                        if (!CharacterIsGet(_drawCharacter)) GameManager.I.DataManager.DataWrapper.CharacterInventory.Add(_drawCharacter);
                        else
                        {
                            int inventoryOrder = FindInventoryOrder(_drawCharacter);
                            StarExpUp(inventoryOrder);
                        }
                        break;
                    }
                    else continue;
                }
            }
            else if(CharacterRank == 1) // A·©Å©
            {
                if (_dataWrapper.CharacterDatas[CharacterNum].CharacterRank != CharacterData.Rank.A) continue;
                else
                {
                    if (randomValue <= 25)  // A·©Å© È®·ü
                    {
                        _drawCharacter = _dataWrapper.CharacterDatas[CharacterNum];

                        if (!CharacterIsGet(_drawCharacter)) GameManager.I.DataManager.DataWrapper.CharacterInventory.Add(_drawCharacter);
                        else
                        {
                            int inventoryOrder = FindInventoryOrder(_drawCharacter);
                            StarExpUp(inventoryOrder);
                        }
                        break;
                    }
                    else continue;
                }
            }
            else if(CharacterRank == 2) // S·©Å©
            {
                if (_dataWrapper.CharacterDatas[CharacterNum].CharacterRank != CharacterData.Rank.S) continue;
                else
                {
                    if (randomValue <= 4)  // S·©Å© È®·ü
                    {
                        _drawCharacter = _dataWrapper.CharacterDatas[CharacterNum];

                        if (!CharacterIsGet(_drawCharacter)) GameManager.I.DataManager.DataWrapper.CharacterInventory.Add(_drawCharacter);
                        else
                        {
                            int inventoryOrder = FindInventoryOrder(_drawCharacter);
                            StarExpUp(inventoryOrder);
                        }
                        break;
                    }
                    else continue;
                }
            }
            else if (CharacterRank == 3) // SS·©Å©
            {
                if (_dataWrapper.CharacterDatas[CharacterNum].CharacterRank != CharacterData.Rank.SS) continue;
                else
                {
                    if (randomValue <= 1)  // SS·©Å© È®·ü
                    {
                        _drawCharacter = _dataWrapper.CharacterDatas[CharacterNum];

                        if (!CharacterIsGet(_drawCharacter)) GameManager.I.DataManager.DataWrapper.CharacterInventory.Add(_drawCharacter);
                        else
                        {
                            int inventoryOrder = FindInventoryOrder(_drawCharacter);
                            StarExpUp(inventoryOrder);
                        }
                        break;
                    }
                    else continue;
                }
            }
        }
    }

    public void HeroPanelActive()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        DrawCharacter();
        _drawCharacterText.text = _drawCharacter.KoreaTag.ToString();
        _heroPanelSlotRawImage.texture = Resources.Load<Texture>("RenderTextures/CharacterRenderTexture_Slot_" + _drawCharacter.Tag.ToString());

        if (_drawCharacter.CharacterRank == CharacterData.Rank.B)
        {
            _heroPanelFrameImage.sprite = _frameImages[0];
            _heroPanelSlotImage.color = new Color(40 / 255f, 36 / 255f, 29 / 255f, 255 / 255f);
        }
        else if (_drawCharacter.CharacterRank == CharacterData.Rank.A)
        {
            _heroPanelFrameImage.sprite = _frameImages[1];
            _heroPanelSlotImage.color = new Color(20 / 255f, 46 / 255f, 34 / 255f, 255 / 255f);
        }
        else if (_drawCharacter.CharacterRank == CharacterData.Rank.S)
        {
            _heroPanelFrameImage.sprite = _frameImages[2];
            _heroPanelSlotImage.color = new Color(39 / 255f, 10 / 255f, 8 / 255f, 255 / 255f);
        }
        else if (_drawCharacter.CharacterRank == CharacterData.Rank.SS)
        {
            _heroPanelFrameImage.sprite = _frameImages[3];
            _heroPanelSlotImage.color = new Color(33 / 255f, 13 / 255f, 52 / 255f, 255 / 255f);
        }

        _heroPanel.SetActive(true);
    }

    public void HeroPanelInactive()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        _heroPanel.SetActive(false);
    }

    private int FindInventoryOrder(CharacterData data)
    {
        int count = _inventory.Count;

        for (int i = 0; i < count; i++)
        {
            if (data.Tag == _inventory[i].Tag) return i;
            else continue;
        }

        return 0;
    }

    private void StarExpUp(int inventoryOrder)
    {
        GameManager.I.DataManager.DataWrapper.CharacterInventory[inventoryOrder].CurrentStarExp++;

        if (_dataWrapper.CharacterInventory[inventoryOrder].MaxStarExp == _dataWrapper.CharacterInventory[inventoryOrder].CurrentStarExp)
        {
            GameManager.I.DataManager.DataWrapper.CharacterInventory[inventoryOrder].Star++;
            GameManager.I.DataManager.DataWrapper.CharacterInventory[inventoryOrder].CurrentStarExp = 1;
            GameManager.I.DataManager.DataWrapper.CharacterInventory[inventoryOrder].MaxStarExp = 3 * _dataWrapper.CharacterInventory[inventoryOrder].Star;
        }
    }
    #endregion
}
