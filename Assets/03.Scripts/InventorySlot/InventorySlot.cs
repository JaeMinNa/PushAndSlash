using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private int _characterNum;
    private CharacterData _slotData;
    private GameObject _equipFrame;
    private DataWrapper _dataWrapper;
    private bool _isEquip;
    private List<CharacterData> _inventory;

    private void Start()
    {
        _dataWrapper = GameManager.I.DataManager.DataWrapper;
        _slotData = GameManager.I.DataManager.DataWrapper.CharacterDatas[_characterNum];
        _inventory = _dataWrapper.CharacterInventory;
        _equipFrame = transform.parent.parent.transform.GetChild(1).transform.gameObject;
        _isEquip = false;

        if (!CharacterIsGet()) transform.GetComponent<RawImage>().color = new Color(20 / 255f, 20 / 255f, 20 / 255f, 255 / 255f);
        else
        {
            transform.GetComponent<RawImage>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);

            if (!CharacterIsEquip())
            {
                _equipFrame.SetActive(false);
                _isEquip = false;
            }
            else
            {
                _equipFrame.SetActive(true);
                _isEquip = true;
            }
        }
    }

    private void OnEnable()
    {
        if (_dataWrapper == null) return;

        if (!CharacterIsGet()) transform.GetComponent<RawImage>().color = new Color(20 / 255f, 20 / 255f, 20 / 255f, 255 / 255f);
        else
        {
            transform.GetComponent<RawImage>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);

            if (!CharacterIsEquip())
            {
                _equipFrame.SetActive(false);
                _isEquip = false;
            }
            else
            {
                _equipFrame.SetActive(true);
                _isEquip = true;
            }
        }
    }

    private void Update()
    {
        if (CharacterIsGet() && !CharacterIsEquip() && !_isEquip)
        {
            _equipFrame.SetActive(false);
            _isEquip = true;
        }
        else if(CharacterIsGet() && CharacterIsEquip() && _isEquip)
        {
            _equipFrame.SetActive(true);
            _isEquip = false;
        }
    }

    private bool CharacterIsGet()
    {
        for (int i = 0; i < _inventory.Count; i++)
        {
            if (_slotData.Tag == _inventory[i].Tag) return true;
        }

        return false;
    }

    private bool CharacterIsEquip()
    {
        for (int i = 0; i < _inventory.Count; i++)
        {
            if (_slotData.Tag == _inventory[i].Tag)
            {
                if (_inventory[i].IsEquip == true) return true;
                else return false;
            }
        }

        return false;
    }
}
