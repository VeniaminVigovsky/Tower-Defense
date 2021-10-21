using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class SceneLoader : MonoBehaviour
{
    private GameObject _loadingScreenPanel;
    private Image _loadingBar;

    public static SceneLoader Instance;
    public static event Action SceneChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        _loadingScreenPanel = transform.Find("LoadingScreenPanel").gameObject;
        _loadingBar = _loadingScreenPanel?.transform.Find("Bar")?.GetComponent<Image>() ?? null;
        _loadingScreenPanel?.SetActive(false);        
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(int sceneIndex)
    {      

        _loadingScreenPanel?.SetActive(true);
        StartCoroutine(LoadSceneAsync(sceneIndex));
    }

    private IEnumerator LoadSceneAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            if (_loadingBar != null)
            {
                float progressPercent = operation.progress / 0.9f;
                _loadingBar.fillAmount = Mathf.InverseLerp(1.0f, 0.0f, progressPercent);
            }
            yield return null;
        }

        _loadingScreenPanel?.SetActive(false);
        SceneChanged?.Invoke();
        yield break;
    }
}
