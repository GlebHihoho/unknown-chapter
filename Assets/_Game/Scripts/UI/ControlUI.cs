using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ControlUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI mainMenuText;
    [SerializeField] TextMeshProUGUI inventoryText;
    [SerializeField] TextMeshProUGUI characterTabText;
    [SerializeField] TextMeshProUGUI actionText;
    [SerializeField] TextMeshProUGUI moveText;
    [SerializeField] TextMeshProUGUI highlightText;
    [SerializeField] TextMeshProUGUI pauseText;
    [SerializeField] TextMeshProUGUI quicksaveText;
    [SerializeField] TextMeshProUGUI quickloadText;

    [Space]
    [SerializeField] Button mainMenuButton;
    [SerializeField] Button inventoryButton;
    [SerializeField] Button characterTabButton;
    [SerializeField] Button actionButton;
    [SerializeField] Button moveButton;
    [SerializeField] Button highlightButton;
    [SerializeField] Button pauseButton;
    [SerializeField] Button quicksaveButton;
    [SerializeField] Button quickloadButton;

    [Space]
    [SerializeField] GameObject pressKeyToRebind;

    [Space]
    [SerializeField] Button restoreDefaultButton;


    private void Awake()
    {
        mainMenuButton.onClick.AddListener(() => { RebindBinding(GameControls.Bindings.MainMenu); });
        inventoryButton.onClick.AddListener(() => { RebindBinding(GameControls.Bindings.Inventory); });
        characterTabButton.onClick.AddListener(() => { RebindBinding(GameControls.Bindings.CharacterTab); });
        actionButton.onClick.AddListener(() => { RebindBinding(GameControls.Bindings.Action); });
        moveButton.onClick.AddListener(() => { RebindBinding(GameControls.Bindings.Move); });
        highlightButton.onClick.AddListener(() => { RebindBinding(GameControls.Bindings.Highlight); });
        pauseButton.onClick.AddListener(() => { RebindBinding(GameControls.Bindings.Pause); });
        quicksaveButton.onClick.AddListener(() => { RebindBinding(GameControls.Bindings.Quicksave); });
        quickloadButton.onClick.AddListener(() => { RebindBinding(GameControls.Bindings.Quickload); });

        restoreDefaultButton.onClick.AddListener(RestoreDefaults);
    }


    void Start()
    {
        UpdateVisuals();
        HidePressKey();

        GameConsole.instance.OnRestoreDefaultControls += RestoreDefaults;
    }


    private void OnDestroy()
    {
        GameConsole.instance.OnRestoreDefaultControls -= RestoreDefaults;
    }


    private void UpdateVisuals()
    {
        mainMenuText.text = GameControls.instance.GetBindingName(GameControls.Bindings.MainMenu);
        inventoryText.text = GameControls.instance.GetBindingName(GameControls.Bindings.Inventory);
        characterTabText.text = GameControls.instance.GetBindingName(GameControls.Bindings.CharacterTab);
        actionText.text = GameControls.instance.GetBindingName(GameControls.Bindings.Action);
        moveText.text = GameControls.instance.GetBindingName(GameControls.Bindings.Move);
        highlightText.text = GameControls.instance.GetBindingName(GameControls.Bindings.Highlight);
        pauseText.text = GameControls.instance.GetBindingName(GameControls.Bindings.Pause);
        quicksaveText.text = GameControls.instance.GetBindingName(GameControls.Bindings.Quicksave);
        quickloadText.text = GameControls.instance.GetBindingName(GameControls.Bindings.Quickload);
    }

    private void ShowPressKey()
    {
        pressKeyToRebind.SetActive(true);
    }

    private void HidePressKey()
    {
        pressKeyToRebind.SetActive(false);
    }

    private void RebindBinding(GameControls.Bindings binding)
    {
        ShowPressKey();
        GameControls.instance.RebindBinding(binding, () => 
        {
            HidePressKey();
            UpdateVisuals();
        });
    }


    private void RestoreDefaults() => GameControls.instance.RestoreDefault(UpdateVisuals);
}
