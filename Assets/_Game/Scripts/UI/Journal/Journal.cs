using UnityEngine;
using PixelCrushers.DialogueSystem;
using System;
using System.Collections.Generic;
using TMPro;

public class Journal : MonoBehaviour
{
    [SerializeField] GameObject mainPanel;

    [SerializeField] ButtonUpdated journalButton;

    [SerializeField] GameObject recordsPanel;
    [SerializeField] JournalRecord recordPrefab;

    [SerializeField] TextMeshProUGUI description;

    Dictionary<string, JournalRecord> records = new Dictionary<string, JournalRecord>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CloseJournal();
        ResetJournal();

        QuestsEvents.OnQuestChanged += UpdateQuest;
        QuestsEvents.OnEntryStateChange += QuestEntryChange;
    }


    private void OnDestroy()
    {
        QuestsEvents.OnQuestChanged -= UpdateQuest;
        QuestsEvents.OnEntryStateChange -= QuestEntryChange;
    }


    public void OpenJournal() => mainPanel.SetActive(true);
    public void CloseJournal() => mainPanel.SetActive(false);

    public void ToggleJournal() => mainPanel.SetActive(!mainPanel.activeSelf);


    private void ResetJournal()
    {
        records.Clear();
        foreach (Transform item in recordsPanel.transform)
        {
            Destroy(item.gameObject);
        }

        string[] activeQuests = QuestLog.GetAllQuests(QuestState.Active);
        string[] completedQuests = QuestLog.GetAllQuests(QuestState.Success | QuestState.Failure);


        void FillQuests(string[] quests)
        {
            foreach (string quest in quests)
            {
                AddQuest(quest);
            }
        }

        FillQuests(activeQuests);
        FillQuests(completedQuests);

       // recordsPanel.transform.GetComponentInChildren<JournalRecord>().SelectRecord();
    }


    private void UpdateQuest(string name)
    {
        if (records.ContainsKey(name)) records[name].UpdateQuest();
        else AddQuest(name);
    }


    private void AddQuest(string name)
    {
        JournalRecord record = Instantiate(recordPrefab, recordsPanel.transform);
        record.SetQuest(name, this);

        records.Add(name, record);
    }

    public void SelectQuest(string quest)
    {
        description.text = QuestLog.GetQuestDescription(quest);
    }


    private void QuestEntryChange(QuestEntryArgs obj)
    {
        Debug.Log("Quest entry change " + obj.questName + obj.entryNumber);
    }
}
