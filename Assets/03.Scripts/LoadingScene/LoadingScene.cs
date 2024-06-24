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
        int index = Random.Range(0, 6);

        if (index == 0)
        {
            _LoadingDescriptionText.text = "��Ƽ ������ ���� 1:1 ������ ��⼼��.";
        }
        else if (index == 1)
        {
            _LoadingDescriptionText.text = "��Ƽ �������� �¸��ؼ� ��ŷ�� �÷�������.";
        }
        else if (index == 2)
        {
            _LoadingDescriptionText.text = "�پ��� ������ ���� ĳ���͸� �����ϼ���.";
        }
        else if (index == 3)
        {
            _LoadingDescriptionText.text = "���������� ���� ����߸�����.";
        }
        else if (index == 4)
        {
            _LoadingDescriptionText.text = "�������� ���� ĳ���͸� �����ϼ���.";
        }
        else if (index == 5)
        {
            _LoadingDescriptionText.text = "�̱⸦ ���� ������ ĳ���͸� ������ �ڵ����� ��ȭ�˴ϴ�.";
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