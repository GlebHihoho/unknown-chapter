using PixelCrushers.DialogueSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabbedQuestLogWindow : StandardUIQuestLogWindow
{
    [Header("Group Tabs")]
    public Button mainMissionTabButton;
    public Button sideQuestsTabButton;
    public string currentGroup = "Main";

    public void ShowGroup(string group)
    {
        currentGroup = group;
        ShowQuests(QuestState.Active);
    }

    protected override void ShowQuests(QuestState questStateMask)
    {
        // Set tab buttons: (current tab should be non-interactable since it's already selected)
        var showActiveQuests = questStateMask == QuestState.Active;
        var showMainQuests = currentGroup == "Main" && showActiveQuests;
        var showSideQuests = currentGroup == "Side" && showActiveQuests;
        mainMissionTabButton.interactable = !showMainQuests;
        sideQuestsTabButton.interactable = !showSideQuests;
        completedQuestsButton.interactable = !showActiveQuests;

        // Show quests only for current tab:
        currentQuestStateMask = questStateMask;
        noQuestsMessage = GetNoQuestsMessage(questStateMask);
        List<QuestInfo> questList = new List<QuestInfo>();
        // (groups aren't used; keeping this here in case you need to group quests in each tab in the future)
        //if (useGroups)
        //{
        //    var records = QuestLog.GetAllGroupsAndQuests(questStateMask, true);
        //    foreach (var record in records)
        //    {
        //        if (!IsQuestVisible(record.questTitle)) continue;
        //        questList.Add(GetQuestInfo(record.groupName, record.questTitle));
        //    }
        //}
        //else
        {
            string[] titles = QuestLog.GetAllQuests(questStateMask, true, null);
            foreach (var title in titles)
            {
                if (!IsQuestVisible(title)) continue;
                if (showActiveQuests && QuestLog.GetQuestGroup(title) != currentGroup) continue;
                questList.Add(GetQuestInfo(string.Empty, title));
            }
        }
        quests = questList.ToArray();
        OnQuestListUpdated();
    }

}
