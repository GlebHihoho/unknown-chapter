using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionButton : MonoBehaviour
{

    
    public delegate void OnSelected(Resolution resolution);

    [SerializeField] Button button;
    [SerializeField] TextMeshProUGUI label;

    Resolution resolution;

    OnSelected onSelected;
 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(Resolution resolution, OnSelected selected)
    {
        this.resolution = resolution;

        label.text = resolution.width +" X " + resolution.height;
        onSelected = selected;

        button.onClick.AddListener(Selected);

      //  if (Screen.currentResolution.width == resolution.width && Screen.currentResolution.height == resolution.height) button.Select();
    }


    private void Selected()
    {
        onSelected?.Invoke(resolution);
    }
}
