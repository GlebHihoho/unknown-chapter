using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class SavesList : MonoBehaviour
{

    [SerializeField] SaveListEntry entryPrefab;
    [SerializeField] RectTransform content;


    List<SaveListEntry> saves = new List<SaveListEntry>();

    ScrollRect scrollRect;

    WaitForEndOfFrame waitFrame = new WaitForEndOfFrame();


    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();

        foreach (SaveListEntry item in content.GetComponentsInChildren<SaveListEntry>())
        {
            saves.Add(item);
        }
    }


    private void OnEnable()
    {
        RefreshList();

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
            saves[i].SetName(i + 1, key, SaveManager.instance.SavesInfo[key]);
            saves[i].SetActive(true);
            i++;
        }

        StartCoroutine(SelectLast());

    }

    IEnumerator SelectLast()
    {
        yield return waitFrame;

        if (saves.Count > 0) saves[saves.Count - 1].Select();

        scrollRect.verticalNormalizedPosition = 0;
    }


}
