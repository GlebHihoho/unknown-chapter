using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ControlUI : MonoBehaviour
{

    [SerializeField] ChangeKeyButton[] buttons;

    [Space]
    [SerializeField] RebindPrompt pressKeyToRebind;
    [SerializeField] Button restoreDefaultButton;


    private void Awake()
    {

        foreach (ChangeKeyButton button in buttons) 
        {
            button.Init(ShowPressKey, RebindFinished);
        }

        restoreDefaultButton.onClick.AddListener(RestoreDefaults);
    }

    void Start()
    {
        UpdateVisuals();
        HidePressKey();

        GameConsole.instance.OnRestoreDefaultControls += RestoreDefaults;
    }

    private void OnDestroy() => GameConsole.instance.OnRestoreDefaultControls -= RestoreDefaults;


    private void UpdateVisuals()
    {
        foreach (ChangeKeyButton button in buttons)
        {
            button.UpdateVisuals();
        }
    }


    private void ShowPressKey(string actionLabel) => pressKeyToRebind.Show(actionLabel);
    private void HidePressKey() => pressKeyToRebind.Hide();


    private void RebindFinished()
    {
        HidePressKey();
        UpdateVisuals();
    }


    private void RestoreDefaults() => GameControls.instance.RestoreDefault(UpdateVisuals);
}
