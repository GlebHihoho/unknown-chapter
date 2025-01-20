using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VideoSettingsUI : MonoBehaviour
{

    [SerializeField] GameObject resolutionPanel;
    [SerializeField] GameObject resolutionsList;
    [SerializeField] ResolutionButton resolutionButtonPrefab;

    [SerializeField] Button resolutionSelectButton;
    [SerializeField] TextMeshProUGUI resolutionLabel;

    [SerializeField] SelectorUI screenMode;

    bool isFullScreen = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int width = 0, height = 0;
        foreach (Resolution resolution in Screen.resolutions)
        {
            if (width != resolution.width && height != resolution.height) 
            { 
                width = resolution.width;
                height = resolution.height;

                ResolutionButton button = Instantiate(resolutionButtonPrefab, resolutionsList.transform);
                button.Init(resolution, SelectResolution);
            }
        }

        resolutionSelectButton.onClick.AddListener(ShowResolution);
        resolutionLabel.text = Screen.currentResolution.width.ToString() + " X " + Screen.currentResolution.height.ToString();

        resolutionPanel.SetActive(false);

        isFullScreen = Screen.fullScreen;



    }

    private void OnEnable()
    {
        if (isFullScreen) screenMode.Select(1);
        else screenMode.Select(0);
    }


    public void ShowResolution()
    {
        resolutionPanel.SetActive(true);
    }

    public void SelectResolution(Resolution resolution)
    {
        resolutionPanel.SetActive(false);

        SetResolution(resolution.width, resolution.height, isFullScreen);      
    }

    public void SetScreenMode(int index)
    {
        isFullScreen = index == 1;

        SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, isFullScreen);
    }

    private void SetResolution(int width, int height, bool isFullScreen)
    {

        if (isFullScreen) Screen.SetResolution(width, height, FullScreenMode.ExclusiveFullScreen);
        else Screen.SetResolution(width, height, FullScreenMode.Windowed);

        resolutionLabel.text = width.ToString() + " X " + height.ToString();
    }
}
