using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private int _characterNum;
    private CharacterData _slotData;
    private GameObject _equipFrame;
    private bool _isEquip;

    private void Start()
    {
        _slotData = GameManager.I.DataManager.DataWrapper.CharacterDatas[_characterNum];
        _equipFrame = transform.parent.parent.transform.GetChild(1).transform.gameObject;
        _isEquip = false;

        if (!_slotData.IsGet) transform.GetComponent<RawImage>().color = new Color(20 / 255f, 20 / 255f, 20 / 255f, 255 / 255f);
        else transform.GetComponent<RawImage>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);

        if (!_slotData.IsEquip)
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

    private void Update()
    {
        if (!_slotData.IsEquip && !_isEquip)
        {
            _equipFrame.SetActive(false);
            _isEquip = true;
        }
        else if(_slotData.IsEquip && _isEquip)
        {
            _equipFrame.SetActive(true);
            _isEquip = false;
        }
    }
}
