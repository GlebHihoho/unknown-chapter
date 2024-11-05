using TMPro;
using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;

[RequireComponent(typeof(Button))]
public class JournalRecord : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questLabel;
    [SerializeField] Image questStatus;
    [SerializeField] Image recordSelected;

    [Header("Bullet Point Sprites")]
    [SerializeField] Sprite unselected;
    [SerializeField] Sprite selected;

    [SerializeField] Sprite successed;
    [SerializeField] Sprite failed;


    QuestState questState;

    Button button;
    Journal journal;

    string questName;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SelectRecord);
    }

    private void OnDestroy() => button.onClick.RemoveListener(SelectRecord);



    public void SetQuest(string name, Journal journal)
    {
        questName = name;    
        this.journal = journal;

        questLabel.text = QuestLog.GetQuestTitle(questName);
        questState = QuestLog.GetQuestState(questName);

        UpdateVisuals();
    }

    public void UpdateQuest()
    {
        questState = QuestLog.GetQuestState(questName);
        UpdateVisuals();
    }

    public void SelectRecord()
    {
        journal.SelectQuest(questName);
    }


    private void UpdateVisuals()
    {
        switch (questState)
        {
            case QuestState.Unassigned:
                break;
            case QuestState.Active:
                questStatus.sprite = selected;
                break;
            case QuestState.Success:
                questStatus.sprite = successed;
                break;
            case QuestState.Failure:
                questStatus.sprite = failed;
                break;
            case QuestState.Abandoned:
                break;
            case QuestState.Grantable:
                break;
            case QuestState.ReturnToNPC:
                break;
            default:
                break;
        }
    }
}
