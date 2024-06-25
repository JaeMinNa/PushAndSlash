using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingScene : MonoBehaviour
{
    public static string NextScene;
    [SerializeField] Slider _loadingBar;
    [SerializeField] TMP_Text _loadingText;
    [SerializeField] TMP_Text _LoadingDescriptionText;

    private void Start()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadScene());
        LoadingDescription();
    }

    private void Update()
    {
        _loadingText.text = "Loading... " + (int)(_loadingBar.value * 100) + "%";
    }

    public static void LoadScene(string sceneName)
    {
        NextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    private void LoadingDescription()
    {
        int index = Random.Range(0, 5);

        if (index == 0)
        {
            _LoadingDescriptionText.text = "멀티 대전을 통해 1:1 대전을 즐기세요.";
        }
        else if (index == 1)
        {
            _LoadingDescriptionText.text = "멀티 대전에서 승리해서 랭킹을 올려보세요.";
        }
        else if (index == 2)
        {
            _LoadingDescriptionText.text = "다양한 개성을 가진 캐릭터를 수집하세요.";
        }
        else if (index == 3)
        {
            _LoadingDescriptionText.text = "전략적으로 적을 떨어뜨리세요.";
        }
        else if (index == 4)
        {
            _LoadingDescriptionText.text = "캐릭터의 랭크가 더 높을수록 더 강합니다.";
        }
    }

    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(NextScene);
        op.allowSceneActivation = false;
        float timer = 0.0f;

        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
           
            if (op.progress < 0.9f)
            {
                _loadingBar.value = Mathf.Lerp(_loadingBar.value, op.progress, timer);
                if (_loadingBar.value >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                _loadingBar.value = Mathf.Lerp(_loadingBar.value, 1f, timer);
                if (_loadingBar.value == 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}