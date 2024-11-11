using UnityEngine;
using PixelCrushers.DialogueSystem;
using System;
using System.Collections.Generic;
using TMPro;
using System.Text;
using UnityEngine.UI;

public class Journal : MonoBehaviour
{
    [SerializeField] GameObject mainPanel;

    [SerializeField] ButtonUpdated journalButton;

    [SerializeField] GameObject recordsPanel;
    [SerializeField] JournalRecord recordPrefab;

    [SerializeField] TextMeshProUGUI description;

    [SerializeField] JournalSoundData sounds;

    Dictionary<string, JournalRecord> records = new();

    string activeQuest = string.Empty;

    struct Quest
    {
        public QuestState state;
        public HashSet<int> entriesChanged;
    }


    Dictionary<string, Quest> questsChanged = new();


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


    public void OpenJournal()
    {
        mainPanel.SetActive(true);
        journalButton.ResetUpdate();
    }

    public void CloseJournal()
    {
        mainPanel.SetActive(false);
        ClearActiveQuestRecord();
    }

    public void ToggleJournal()
    {
        mainPanel.SetActive(!mainPanel.activeSelf);

        if (!mainPanel.activeSelf) ClearActiveQuestRecord();
        else journalButton.ResetUpdate();
    }

    private void ResetJournal()
    {
        records.Clear();
        foreach (Transform item in recordsPanel.transform)
        {
            Destroy(item.gameObject);
        }


        string[] activeQuests = QuestLog.GetAllQuests(QuestState.Active, true);
        string[] completedQuests = QuestLog.GetAllQuests(QuestState.Success | QuestState.Failure, true);


        void FillQuests(string[] quests, bool isActive)
        {
            foreach (string quest in quests)
            {
                AddQuest(quest);
            }
        }

        FillQuests(activeQuests, true);
        FillQuests(completedQuests, false);

       
    }


    private void UpdateQuest(string name)
    {
        if (records.ContainsKey(name)) 
        { 
            //records[name].UpdateQuest(); 
        }
        else AddQuest(name);

        UpdateQuestRecord(name);

        if (name == activeQuest) SelectQuest(name);

        SoundManager.instance.PlayEffect(sounds.NewNote);
        journalButton.ShowUpdate();
    }


    private void AddQuest(string name)
    {
        JournalRecord record = Instantiate(recordPrefab, recordsPanel.transform);
        record.SetQuest(name, this);

        records.Add(name, record);

        UpdateQuestRecord(name);

        if (name == activeQuest) SelectQuest(name);
    }


    private void UpdateQuestRecord(string name)
    {
        QuestState state = QuestLog.GetQuestState(name);

        if (!questsChanged.ContainsKey(name))
        {
            Quest quest;
            quest.state = state;
            quest.entriesChanged = new();// HashSet<int> { 0 };

            questsChanged.Add(name, quest);
        }
        else if (questsChanged[name].state != state)
        {
            Quest quest = questsChanged[name];
            quest.state = state;
            quest.entriesChanged.Add(0);

            questsChanged[name] = quest;
        }
    }

    private void ClearActiveQuestRecord()
    {
        if (questsChanged.ContainsKey(activeQuest))
        {
            if (questsChanged[activeQuest].state != QuestState.Active) questsChanged.Remove(activeQuest);
            else questsChanged[activeQuest].entriesChanged.Clear();
        }
    }


    public void SelectQuest(string quest)
    {
        const string newTextStart = "<color=blue><b>(Новое)</b> ";
        const string newTextEnd = "</color>";

        if (quest != activeQuest) 
        { 
            ClearActiveQuestRecord();           
            activeQuest = quest; 
        }

        StringBuilder sb = new StringBuilder();

        sb.Append("<color=black>");

        HashSet<int> newEntires = new();

        if (questsChanged.ContainsKey (quest))
        {
            newEntires = questsChanged[quest].entriesChanged;
        }

        if (newEntires.Contains(0)) sb.Append(newTextStart);

        sb.AppendLine(QuestLog.GetQuestDescription(quest));

        if (newEntires.Contains(0)) sb.Append(newTextEnd);


        int entryCount = DialogueLua.GetQuestField(quest, "Entry Count").asInt;
        for (int i = 1; i <= entryCount; i++)
        {
            sb.AppendLine("***");

            string state = DialogueLua.GetQuestField(quest, $"Entry {i} State").asString.ToLower();
  
            string entryText = DialogueLua.GetQuestField(quest, $"Entry {i} {state}").asString;

            if (entryText == string.Empty) entryText = DialogueLua.GetQuestField(quest, $"Entry {i}").asString;

            if (newEntires.Contains(i)) sb.Append(newTextStart);

            sb.AppendLine(entryText);

            if (newEntires.Contains(i)) sb.Append(newTextEnd);

        }

        /*
        for (int i = 0; i < 100; i++)
        {
            sb.AppendLine(i.ToString());
        }
        */


        description.text = sb.ToString();

    }


    private void QuestEntryChange(QuestEntryArgs obj)
    {
        if (questsChanged.ContainsKey(obj.questName))
        {
            questsChanged[obj.questName].entriesChanged.Add(obj.entryNumber);
        }

        if (obj.questName == activeQuest) SelectQuest(activeQuest);
    }
}
