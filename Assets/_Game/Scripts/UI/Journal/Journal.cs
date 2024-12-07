using UnityEngine;
using PixelCrushers.DialogueSystem;
using System;
using System.Collections.Generic;
using TMPro;
using System.Text;
using UnityEngine.UI;
using System.Linq;

public class Journal : MonoBehaviour, ISaveable
{
    [SerializeField] GameObject mainPanel;

    [SerializeField] ButtonUpdated journalButton;

    [SerializeField] GameObject recordsPanel;
    [SerializeField] JournalRecord recordPrefab;

    [SerializeField] TextMeshProUGUI description;

    [SerializeField] JournalSoundData sounds;

    Dictionary<string, JournalRecord> records = new();
    List<string> recordsNames = new();

    [SerializeField, TextArea(0, 3)] string newTextStart;
    [SerializeField, TextArea(0, 3)] string newTextEnd;

    [SerializeField, QuestPopup] string initialQuest;

    public bool JournalOpened
    {
        get { return mainPanel.activeSelf; }
    }

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

        if (recordsNames.Count == 0) AddQuest(initialQuest);

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
        recordsNames.Clear();
        foreach (Transform item in recordsPanel.transform)
        {
            Destroy(item.gameObject);
        }      
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

        if (records.ContainsKey(name))
        {
            Debug.LogWarning($"Quest {name} already added.");
            return;
        }

        JournalRecord record = Instantiate(recordPrefab, recordsPanel.transform);
        record.SetQuest(name, this);

        records.Add(name, record);
        recordsNames.Add(name);
    }


    private void UpdateQuestRecord(string name)
    {
        QuestState state = QuestLog.GetQuestState(name);

        if (!questsChanged.ContainsKey(name))
        {
            Quest quest;
            quest.state = state;
            quest.entriesChanged = new() { 0 };

            int entryCount = DialogueLua.GetQuestField(name, "Entry Count").asInt;
            for (int i = 1; i <= entryCount; i++)
            {
                string entryState = DialogueLua.GetQuestField(name, $"Entry {i} State").asString.ToLower();
                if (entryState == "active") quest.entriesChanged.Add(i);
            }

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

        if (quest != activeQuest) 
        { 
            ClearActiveQuestRecord();           
            activeQuest = quest; 
        }

        StringBuilder sb = new StringBuilder();

        //sb.Append("<color=black>");

        HashSet<int> newEntires = new();

        if (questsChanged.ContainsKey (quest))
        {
            newEntires = questsChanged[quest].entriesChanged;
        }

             
        string questDescription = QuestLog.GetQuestDescription(quest);

        if (questDescription != null)
        {
            if (newEntires.Contains(0)) sb.Append(newTextStart);
            sb.AppendLine(questDescription);          
            if (newEntires.Contains(0)) sb.Append(newTextEnd);
        }

        bool firstText = questDescription == null;

        int entryCount = DialogueLua.GetQuestField(quest, "Entry Count").asInt;
        for (int i = 1; i <= entryCount; i++)
        {


            string state = DialogueLua.GetQuestField(quest, $"Entry {i} State").asString.ToLower();

            if (state != "unassigned")
            {

                string entryText = DialogueLua.GetQuestField(quest, $"Entry {i} {state}").asString;

                string query = $"Entry {i} {state}";

                if (entryText == string.Empty) entryText = DialogueLua.GetQuestField(quest, $"Entry {i}").asString;

                if (!firstText)
                {
                    sb.AppendLine("");
                    sb.AppendLine("<align=\"center\">* * *</align>");
                }

                if (firstText) firstText = false;

                if (newEntires.Contains(i)) sb.Append(newTextStart);

                sb.AppendLine(entryText);

                if (newEntires.Contains(i)) sb.Append(newTextEnd);
            }

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

    public void Save(ref SaveData.Save save)
    {
        save.journalActiveQuest = activeQuest;

        save.journalRecords = recordsNames;

        save.recordStatus.Clear();

        foreach (string key in questsChanged.Keys)
        {
            SaveData.RecordStatus rec;

            rec.id = key;
            rec.state = questsChanged[key].state;
            rec.entriesChanged = new(questsChanged[key].entriesChanged.ToList());

            save.recordStatus.Add(rec);
        }
    }

    public void Load(SaveData.Save save)
    {
        CloseJournal();
        ResetJournal();

        if (save.journalRecords.Count == 0) AddQuest(initialQuest);
        else
        {
            foreach (string record in save.journalRecords)
            {
                AddQuest(record);
            }


            foreach (string key in records.Keys)
            {
                records[key].SetHaveUpdates(false);
            }
            
            questsChanged.Clear();
            foreach (SaveData.RecordStatus record in save.recordStatus)
            {
                Quest quest;
                quest.state = record.state;
                quest.entriesChanged = new(record.entriesChanged.ToHashSet());

                if (!questsChanged.ContainsKey(record.id)) questsChanged.Add(record.id, quest);
                else Debug.LogWarning($"Changed quest already contains key {record.id}");

                records[record.id].SetHaveUpdates(record.entriesChanged.Count > 0);
            }
            
        }

        if (records.ContainsKey(save.journalActiveQuest)) records[save.journalActiveQuest].SelectRecord();
    }
}
