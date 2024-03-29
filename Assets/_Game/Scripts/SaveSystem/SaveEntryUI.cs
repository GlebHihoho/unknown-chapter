using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SaveEntryUI : MonoBehaviour
{

    enum Mode { Save, Load}
    [SerializeField] Mode mode = Mode.Load;

    [Header("Save details")]
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI location;
    [SerializeField] TextMeshProUGUI timeStamp;

    [Header("Controls")]
    [SerializeField] Button actionButton;
    [SerializeField] Button deleteButton;

    [SerializeField] GameObject confirmationPanel;
    [SerializeField] Button yesButton;


    string saveName;


    public static event Action<string> OnSave;
    public static event Action<string> OnLoad;

    public static event Action<string> OnDelete;


    private void OnEnable()
    {
        SaveListEntry.OnSaveSelected += SetEntry;

        SaveUI.OnNameUpdate += NewEntry;

        deleteButton.onClick.AddListener(ShowConfirmation);
        actionButton.onClick.AddListener(Action);

        yesButton.onClick.AddListener(Delete);

        NewEntry("");
    }

    private void OnDisable()
    {
        SaveListEntry.OnSaveSelected -= SetEntry;

        SaveUI.OnNameUpdate -= NewEntry;

        deleteButton.onClick.RemoveListener(ShowConfirmation);

        yesButton.onClick.RemoveListener(Delete);
        actionButton.onClick.RemoveListener(Action);
    }


    private void SetEntry(string saveName)
    {

        this.saveName = saveName;

        SaveManager.Summary info = SaveManager.instance.SavesInfo[saveName];

        location.text = info.location;

        timeStamp.text = info.timeStamp.ToShortDateString() + " - " + info.timeStamp.ToShortTimeString();
    }


    private void NewEntry(string saveName)
    {
        this.saveName = saveName;

        location.text = "";
        timeStamp.text = "";
    }


    private void ShowConfirmation()
    {
        confirmationPanel.SetActive(true);
    }


    private void Action()
    {
        switch (mode)
        {
            case Mode.Save:
                Save();
                break;
            case Mode.Load:
                Load();
                break;
            default:
                break;
        }
    }


    private void Save() => OnSave?.Invoke(saveName);

    private void Load() => OnLoad?.Invoke(saveName);

    private void Delete() => OnDelete?.Invoke(saveName);


}
