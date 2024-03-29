using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SaveUI : MonoBehaviour
{

    [SerializeField] TMP_InputField inputName;

    public static event Action<string> OnNameUpdate;



    private void OnEnable()
    {
        SaveListEntry.OnSaveSelected += SetName;

        inputName.onValueChanged.AddListener(UpdateName);
    }


    private void OnDisable()
    {
        SaveListEntry.OnSaveSelected -= SetName;

        inputName.onValueChanged.RemoveListener(UpdateName);

    }

    private void UpdateName(string s)
    {
        OnNameUpdate?.Invoke(s);
    }


    private void SetName(string saveName)
    {
        inputName.text = saveName;
    }
}
