using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavesList : MonoBehaviour
{

    [SerializeField] SaveListEntry entryPrefab;
    [SerializeField] RectTransform content;


    List<SaveListEntry> saves = new List<SaveListEntry>();


    private void Awake()
    {

        foreach (SaveListEntry item in content.GetComponentsInChildren<SaveListEntry>())
        {
            saves.Add(item);
        }
    }

    private void Start()
    {
        RefreshList();
    }

    private void OnEnable()
    {
        SaveManager.OnSaveAdded += RefreshList;
        SaveManager.OnSaveRemoved += RefreshList;
    }

    private void OnDisable()
    {
        SaveManager.OnSaveAdded -= RefreshList;
        SaveManager.OnSaveRemoved -= RefreshList;
    }



    private void RefreshList(string s) => RefreshList(); // Temporary placeholder for simplicity

    private void RefreshList()
    {

        foreach (SaveListEntry item in saves)
        {
            item.SetActive(false);
        }

        while (saves.Count < SaveManager.instance.SavesInfo.Count)
        {
            SaveListEntry entry = Instantiate(entryPrefab, content);
            saves.Add(entry);
        }

        int i = 0;
        foreach (string key in SaveManager.instance.SavesInfo.Keys)
        {
            saves[i].SetName(key);
            saves[i].SetActive(true);
            i++;
        }

        if (saves.Count > 0) saves[0].Select();

    }


}
