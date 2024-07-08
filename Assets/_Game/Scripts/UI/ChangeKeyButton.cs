using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeKeyButton : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] TextMeshProUGUI keyLabel;
    [SerializeField] TextMeshProUGUI actionLabel;

    [Space]
    [SerializeField] Image keyField;
    [SerializeField] Sprite leftMouseButton;
    [SerializeField] Sprite rightMouseButton;

    Sprite defaultSprite;

    [Space]
    [SerializeField] GameControls.Bindings binding;

    public delegate void RebindActionStarted(string actionLabel);
    public delegate void RebindActionFinished();

    RebindActionStarted actionStarted;
    RebindActionFinished actionFinished;


    private void Awake()
    {
        defaultSprite = keyField.sprite;
    }


    public void Init(RebindActionStarted rebindStarted, RebindActionFinished rebindFinished)
    {
        actionStarted = rebindStarted;
        actionFinished = rebindFinished;

        button.onClick.AddListener(RebindBinding);
    }



    public void UpdateVisuals()
    {
        string s = GameControls.instance.GetBindingName(binding);

        if (s == "leftButton" || s == "LMB") // TODO: Find better way to check bindings to mouse keys
        {
            keyField.sprite = leftMouseButton;
            keyLabel.text = "";
        }
        else if (s == "rightButton" || s == "RMB")
        {
            keyField.sprite = rightMouseButton;
            keyLabel.text = "";
        }
        else 
        {
            keyField.sprite = defaultSprite;
            keyLabel.text = s; 
        }

    }


    private void RebindBinding()
    {
        actionStarted(actionLabel.text);
        GameControls.instance.RebindBinding(binding,
            (() => { actionFinished(); }),
            (() => { actionFinished(); }));

    }
}
