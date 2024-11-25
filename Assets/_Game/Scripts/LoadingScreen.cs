using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen instance;

    [SerializeField] GameObject loadingPanel;

    [SerializeField] Image progress; 

    private void Awake()
    {
        if (instance == null) instance = this;

        loadingPanel.SetActive(false);
    }


    private void Start() => SaveManager.OnLoadCompleted += LoadCompleted;
    private void OnDestroy() => SaveManager.OnLoadCompleted -= LoadCompleted;

    private void LoadCompleted()
    {
        progress.fillAmount = 1;
        StartCoroutine(HidingScreen());
    }

    IEnumerator HidingScreen()
    {
        yield return null;
        ToggleScreen(false);
    }

    private void ToggleScreen(bool isShown)
    {
        loadingPanel.SetActive(isShown);
    }


    public void Load(string sceneName, Action OnLoad = null)
    {
        StartCoroutine(LoadingScene(sceneName, OnLoad));
    }


    IEnumerator LoadingScene(string sceneName, Action OnLoad)
    {
        progress.fillAmount = 0;

        ToggleScreen(true);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
            progress.fillAmount = asyncLoad.progress;
        }

        OnLoad?.Invoke();        
    }

}
