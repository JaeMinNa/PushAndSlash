using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;
using ECM2.Examples.Slide;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("CoolTime Image")]
    [SerializeField] private Image _dashImage;
    [SerializeField] private Image _skillImage;

    private GameObject _player;
    private Animator _playerAnimator;
    private PlayerCharacter _playerCharacter;
    private CharacterData _playerData;
    private float _dashTime;
    private float _skillTime;

    private void Start()
    {
        _player = GameManager.I.PlayerManager.Player;
        _playerCharacter = _player.GetComponent<PlayerCharacter>();
        _playerAnimator = _player.transform.GetChild(0).GetComponent<Animator>();
        _playerData = GameManager.I.DataManager.PlayerData;
        _dashTime = 0f;
        _skillTime = 0f;

        StartCoroutine(COCoolTimeRoutine(_dashImage, _playerData.DashCoolTime));
        StartCoroutine(COCoolTimeRoutine(_skillImage, _playerData.SkillCoolTime));
    }

    private void Update()
    {
        _dashTime += Time.deltaTime;
        _skillTime += Time.deltaTime;
    }

    public void Init()
    {

    }

    public void Release()
    {

    }

    public void PlayerJumpButtonUp()
    {
        _player.GetComponent<Character>().StopJumping();
    }

    public void PlayerJumpButtonDown()
    {
        _player.GetComponent<Character>().Jump();
        //GameManager.I.SoundManager.StartSFX("PlayerJump");
    }

    public void PlayerAttack()
    {
            _playerAnimator.SetTrigger("Attack");
    }

    public void PlayerDashButtonUp()
    {
        _playerCharacter.UnCrouch();
    }

    public void PlayerDashButtonDown()
    {
        if(_dashTime >= _playerData.DashCoolTime)
        {
            StartCoroutine(COCoolTimeRoutine(_dashImage, _playerData.DashCoolTime));
            _playerAnimator.SetTrigger("Dash");
            _playerCharacter.Crouch();
            _dashTime = 0f;
        }
    }

    public void PlayerSkillButton()
    {
        if (_skillTime >= _playerData.SkillCoolTime)
        {
            StartCoroutine(COCoolTimeRoutine(_skillImage, _playerData.SkillCoolTime));
            _playerAnimator.SetTrigger("Skill");
            _skillTime = 0f;
            _playerCharacter.IsSkill = true;
        }
    }

    private IEnumerator COCoolTimeRoutine(Image image, float coolTime)
    {
        float time = coolTime;
        float timer = 0f;
        while (true)
        {
            timer += Time.deltaTime;
            float per = timer / time;
            image.fillAmount = per;

            if (timer >= time)
            {
                image.fillAmount = 1f;
                _playerCharacter.IsSkill = false;
                break;
            }
            yield return null;
        }
    }

    private IEnumerator COIsSkillFalse()
    {
        yield return new WaitForSeconds(0.5f);

        _playerCharacter.IsSkill = false;
    }
}
