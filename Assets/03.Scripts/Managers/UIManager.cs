using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;
using ECM2.Examples.Slide;
using UnityEngine.UI;
using UnityEngine.Audio;

public class UIManager : MonoBehaviour
{
    [Header("CoolTime Image")]
    [SerializeField] private Image _dashImage;
    [SerializeField] private Image _skillImage;

    [Header("Panel")]
    [SerializeField] private GameObject _pause;

    [Header("Setting")]
    [SerializeField] private GameObject _settingPanel;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private Slider _bgmSlider;

    private GameObject _player;
    private Animator _playerAnimator;
    private PlayerCharacter _playerCharacter;
    private CharacterData _playerData;
    private GameData _gameData;
    private float _dashTime;
    private float _skillTime;

    private void Update()
    {
        if (GameManager.I.ScenesManager.CurrentSceneName == "LobbySence")
        {
            
        }
        else
        {
            _dashTime += Time.deltaTime;
            _skillTime += Time.deltaTime;
        }
    }

    public void Init()
    {
        _player = GameManager.I.PlayerManager.Player;
        _playerCharacter = _player.GetComponent<PlayerCharacter>();
        _playerAnimator = _player.transform.GetChild(0).GetComponent<Animator>();
        _playerData = GameManager.I.DataManager.PlayerData;
        _gameData = GameManager.I.DataManager.GameData;

        if (GameManager.I.ScenesManager.CurrentSceneName == "LobbySence")
        {

        }
        else
        {
            _dashTime = 0f;
            _skillTime = 0f;

            StartCoroutine(COCoolTimeRoutine(_dashImage, _playerData.DashCoolTime));
            StartCoroutine(COCoolTimeRoutine(_skillImage, _playerData.SkillCoolTime));
        }


        SoundSetting();
    }

    public void Release()
    {

    }

    private void SoundSetting()
    {
        float sfx = _gameData.SFX;
        float bgm = _gameData.BGM;
        _sfxSlider.value = sfx;
        _bgmSlider.value = bgm;

        if (sfx == -40f)	// -40¿œ ∂ß, ¿Ωæ«¿ª ≤®¡‹
        {
            _audioMixer.SetFloat("SFX", -80f);
        }
        else
        {
            _audioMixer.SetFloat("SFX", sfx);
        }

        if (bgm == -40f)	// -40¿œ ∂ß, ¿Ωæ«¿ª ≤®¡‹
        {
            _audioMixer.SetFloat("BGM", -80f);
        }
        else
        {
            _audioMixer.SetFloat("BGM", bgm);
        }
    }

    public void SFXControl()
    {
        float sound = _sfxSlider.value;
        _gameData.SFX = sound;

        if (sound == -40f)	// -40¿œ ∂ß, ¿Ωæ«¿ª ≤®¡‹
        {
            _audioMixer.SetFloat("SFX", -80f);
        }
        else
        {
            _audioMixer.SetFloat("SFX", sound);
        }
    }

    public void BGMControl()
    {
        float sound = _bgmSlider.value;
        _gameData.BGM = sound;

        if (sound == -40f)	// -40¿œ ∂ß, ¿Ωæ«¿ª ≤®¡‹
        {
            _audioMixer.SetFloat("BGM", -80f);
        }
        else
        {
            _audioMixer.SetFloat("BGM", sound);
        }
    }

    public void PlayerJumpButtonUp()
    {
        _player.GetComponent<Character>().StopJumping();
    }

    public void PlayerJumpButtonDown()
    {
        _player.GetComponent<Character>().Jump();
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
            StartCoroutine(COIsSkillFalse());
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
                //_playerCharacter.IsSkill = false;
                break;
            }
            yield return null;
        }
    }

    private IEnumerator COIsSkillFalse()
    {
        yield return new WaitForSeconds(1f);

        _playerCharacter.IsSkill = false;
    }

    public void PauseStartButton()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        Time.timeScale = 0f;
        _pause.SetActive(true);
    }

    public void PauseStopButton()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        Time.timeScale = 1f;
        _pause.SetActive(false);
    }

    public void SettingActive()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        _settingPanel.SetActive(true);
        _gameData.Stage++;
        _gameData.Coin++;
        _playerData.Level++;
    }

    public void SettingInactive()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        _settingPanel.SetActive(false);
    }

}
