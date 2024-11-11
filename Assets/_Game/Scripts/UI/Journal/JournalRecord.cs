using TMPro;
using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;
using System.Text;

[RequireComponent(typeof(Button))]
public class JournalRecord : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questLabel;
    [SerializeField] Image questStatus;
    [SerializeField] Image recordSelected;

    [Header("Bullet Point Sprites")]
    [SerializeField] Sprite unselected;
    [SerializeField] Sprite selected;

    [SerializeField, Range(0, 1.5f)] float selectedScale = 0.5f;

    [SerializeField] Sprite successed;
    [SerializeField] Sprite failed;

    [SerializeField] Color successColor = Color.green;
    [SerializeField] Color failureColor = Color.red;
    [SerializeField] Color defaultColor = Color.white;



    QuestState questState;

    Button button;
    Journal journal;

    string questName;

    static JournalRecord activeRecord;

    bool haveUpdates = false;

    StringBuilder sb = new();

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SelectRecord);       
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(SelectRecord);
        if (activeRecord == this) activeRecord = null;

        QuestsEvents.OnQuestChanged -= UpdateQuest;
    }

    public void SetQuest(string name, Journal journal)
    {
        questName = name;    
        this.journal = journal;

        questLabel.text = QuestLog.GetQuestTitle(questName);
        questState = QuestLog.GetQuestState(questName);

        if (questState == QuestState.Active) QuestsEvents.OnQuestChanged += UpdateQuest;

        if (activeRecord == null)
        {
            activeRecord = this;
            journal.SelectQuest(name);

        }

        if (activeRecord != this) haveUpdates = true;

        UpdateVisuals();
    }

    private void UpdateQuest(string name)
    {
        if (questName == name)
        {
            questState = QuestLog.GetQuestState(questName);

            if (questState != QuestState.Active)
            {
                QuestsEvents.OnQuestChanged -= UpdateQuest;
            }

            if (activeRecord != this) haveUpdates= true;

            UpdateVisuals();
        }
    }

    public void UpdateQuest()
    {
        questState = QuestLog.GetQuestState(questName);
        UpdateVisuals();
    }

    public void SelectRecord()
    {
        if (activeRecord != this) 
        {
            JournalRecord prevRecord = activeRecord;
            activeRecord = this;
            
            prevRecord.UpdateVisuals();

            haveUpdates = false;

            UpdateVisuals();
        }
        journal.SelectQuest(questName);
    }


    private void UpdateVisuals()
    {
        questStatus.transform.localScale = Vector3.one;

        

        if (questState == QuestState.Success)
        {
            questStatus.sprite = successed;

            questLabel.color = successColor;
            questLabel.fontStyle = FontStyles.Strikethrough;

        }
        else if (questState == QuestState.Failure)
        {
            questStatus.sprite = failed;

            questLabel.color = failureColor;
            questLabel.fontStyle= FontStyles.Strikethrough;
        }
        else if (this == activeRecord)
        {
            questStatus.sprite = selected;
            questStatus.transform.localScale = Vector3.one * selectedScale;

            questLabel.color = defaultColor;
            questLabel.fontStyle = FontStyles.Normal;
        }
        else
        {
            questStatus.sprite = unselected;
            questStatus.transform.localScale = Vector3.one * selectedScale;

            questLabel.color = defaultColor;
            questLabel.fontStyle = FontStyles.Normal;
        }

        recordSelected.enabled = this == activeRecord;

        sb.Clear();
     
        sb.Append(QuestLog.GetQuestTitle(questName));
        if (haveUpdates) sb.Append("<color=blue><b> @</b></color>");

        questLabel.text = sb.ToString();
    }
}
