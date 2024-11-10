using UnityEngine;
using PixelCrushers.DialogueSystem;
using System;
using System.Collections.Generic;
using TMPro;
using System.Text;
using Unity.VisualScripting;

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

        string[] activeQuests = QuestLog.GetAllQuests(QuestState.Active, true);
        string[] completedQuests = QuestLog.GetAllQuests(QuestState.Success | QuestState.Failure, true);


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
        Debug.Log("<color=blue>Updating quest: </color>" + name);
        if (records.ContainsKey(name)) records[name].UpdateQuest();
        else AddQuest(name);
    }


    private void AddQuest(string name)
    {
        Debug.Log("<color=blue>Adding quest: </color>" + name);
        JournalRecord record = Instantiate(recordPrefab, recordsPanel.transform);
        record.SetQuest(name, this);

        records.Add(name, record);
    }

    public void SelectQuest(string quest)
    {

        StringBuilder sb = new StringBuilder();
      
        sb.AppendLine(QuestLog.GetQuestDescription(quest));

        /*
        for (int i = 1; i <= QuestLog.GetQuestEntryCount(quest); i++)
        {
            sb.AppendLine("---");
            sb.Append(QuestLog.GetQuestEntry(quest, i));
            sb.Append(" ");
            sb.AppendLine(QuestLog.GetQuestEntryState(quest, i).ToString());
        }
        */
        sb.AppendLine("***");

        int entryCount = DialogueLua.GetQuestField(quest, "Entry Count").asInt;
        for (int i = 1; i <= entryCount; i++)
        {

            sb.AppendJoin(' ',
                DialogueLua.GetQuestField(quest, $"Entry {i}").asString,
                DialogueLua.GetQuestField(quest, $"Entry {i} State").asString,
                DialogueLua.GetQuestField(quest, $"Entry {i} Active").asString,
                DialogueLua.GetQuestField(quest, $"Entry {i} Success").asString
                );
        }

        /*
        sb.AppendLine("***");
        sb.Append(DialogueLua.GetQuestField("Heal Foma", "Entry Count").asString);
        sb.Append(" ");

        sb.AppendLine(DialogueLua.GetQuestField("Heal Foma", "Entry 1 Active").AsString);

        sb.AppendLine("***");
        sb.AppendLine(DialogueLua.GetItemField("Heal Foma", "Entry 2 Active").IsString.ToString());
        sb.AppendLine("***");
        */

        description.text = sb.ToString();


    }


    private void QuestEntryChange(QuestEntryArgs obj)
    {
        string state = DialogueLua.GetQuestField(obj.questName, $"Entry {obj.entryNumber} State").asString;
        Debug.Log("<Color=green>Quest entry change</color> " + obj.questName + " " + obj.entryNumber + " " + state);
    }
}
