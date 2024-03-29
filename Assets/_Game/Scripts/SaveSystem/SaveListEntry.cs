using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SaveListEntry : MonoBehaviour
{

    [SerializeField] Button button;
    [SerializeField] TextMeshProUGUI saveNameLabel;

    string saveName;

    public static event Action<string> OnSaveSelected;


    private void OnEnable() => button.onClick.AddListener(Select);

    private void OnDisable() => button.onClick.RemoveListener(Select);


    public void SetName(string saveName)
    {
        this.saveName = saveName;

        saveNameLabel.text = saveName;
    }

    public void SetActive(bool isActive) => gameObject.SetActive(isActive);

    public void Select()
    {
        OnSaveSelected?.Invoke(saveName);
    }
}
