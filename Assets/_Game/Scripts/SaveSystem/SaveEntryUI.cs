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

    [SerializeField] Button actionButton;
    [SerializeField] Button deleteButton;

    [SerializeField] ModalWindow modalWindow;


    string saveName;


    public static event Action OnSave;
    public static event Action<string> OnLoad;

    public static event Action<string> OnDelete;

    private void Awake()
    {
        SaveListEntry.OnSaveSelected += SetEntry;
        SaveListEntry.OnDoubleClick += EntryDoubleClick;

        SaveManager.OnSaveListEmptied += ResetEntry;

        actionButton.interactable = mode == Mode.Save;
        deleteButton.interactable = false;
    }


    private void OnDestroy()
    {
        SaveListEntry.OnSaveSelected -= SetEntry;
        SaveListEntry.OnDoubleClick -= EntryDoubleClick;

        SaveManager.OnSaveListEmptied -= ResetEntry;
    }

    private void OnEnable()
    {       
        //SaveUI.OnNameUpdate += NewEntry;

        deleteButton.onClick.AddListener(ShowConfirmation);
        actionButton.onClick.AddListener(Action);
    }


    private void OnDisable()
    {       
        //SaveUI.OnNameUpdate -= NewEntry;

        deleteButton.onClick.RemoveListener(ShowConfirmation);

        actionButton.onClick.RemoveListener(Action);
    }


    private void SetEntry(string saveName, SaveData.Type type)
    {
        this.saveName = saveName;

        actionButton.interactable = true;
        deleteButton.interactable = type == SaveData.Type.Normal;
    }


    private void ResetEntry()
    {
        saveName = string.Empty;

        actionButton.interactable = mode == Mode.Save;
        deleteButton.interactable = false;
    }


    private void EntryDoubleClick(string saveName)
    {
        if (mode == Mode.Load) OnLoad?.Invoke(saveName);
    }

    private void ShowConfirmation()
    {
        modalWindow.ShowPromt("�������� ����������", "�� ������� ��� ������ ������� ����������?", Delete);
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


    private void Save() => OnSave?.Invoke();

    private void Load() => OnLoad?.Invoke(saveName);

    private void Delete(bool isConfirmed)
    {
        if (isConfirmed) OnDelete?.Invoke(saveName);
    }


}
