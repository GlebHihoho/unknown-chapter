using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class GameControls : MonoBehaviour
{

    const string playerBindings = "InputBindings";

    PlayerInputActions inputActions;

    public static GameControls instance;

    public event Action<InputAction.CallbackContext> OnMainMenu;
    public event Action<InputAction.CallbackContext> OnGameConsole;

    public event Action<InputAction.CallbackContext> OnInventory;
    public event Action<InputAction.CallbackContext> OnCharacterTab;
    public event Action<InputAction.CallbackContext> OnAction;
    public event Action<InputAction.CallbackContext> OnMove;
    public event Action<InputAction.CallbackContext> OnHighlightStarted;
    public event Action<InputAction.CallbackContext> OnHighlightEnded;
    public event Action<InputAction.CallbackContext> OnPause;
    public event Action<InputAction.CallbackContext> OnQuicksave;
    public event Action<InputAction.CallbackContext> OnQuickload;

    public enum Bindings { MainMenu, GameConsole, Pause, Inventory, CharacterTab, Action, Move, Highlight, Quicksave, Quickload };



    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);

        inputActions = new();

        if (PlayerPrefs.HasKey(playerBindings)) inputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(playerBindings));


        inputActions.System.Enable();
        inputActions.Player.Enable();

        inputActions.System.MainMenu.performed += MainMenu;
        inputActions.System.GameConsole.performed += GameConsole;
        inputActions.System.Pause.performed += PauseGame;

        inputActions.Player.Inventory.performed += Inventory;
        inputActions.Player.CharacterTab.performed += CharacterTab;
        inputActions.Player.Action.performed += Action;
        inputActions.Player.Move.performed += Move;
        inputActions.Player.Highlight.started += HighlightStarted;
        inputActions.Player.Highlight.canceled += HighlightEnded;
        inputActions.Player.QuickSave.performed += Quicksave;
        inputActions.Player.QuickLoad.performed += Quickload;

    }


    private void OnDestroy()
    {
        if (inputActions != null) 
        {
            inputActions.System.Disable();
            inputActions.Player.Disable(); 
        }
    }

    private void OnEnable() => Pause.OnPause += SetPause;
    private void OnDisable() => Pause.OnPause -= SetPause;


    private void MainMenu(InputAction.CallbackContext obj) => OnMainMenu?.Invoke(obj);
    private void GameConsole(InputAction.CallbackContext obj) => OnGameConsole?.Invoke(obj);
    private void PauseGame(InputAction.CallbackContext obj) => OnPause?.Invoke(obj);

    private void Inventory(InputAction.CallbackContext obj) => OnInventory?.Invoke(obj);
    private void CharacterTab(InputAction.CallbackContext obj) => OnCharacterTab?.Invoke(obj);
    private void Action(InputAction.CallbackContext obj) => OnAction?.Invoke(obj);
    private void Move(InputAction.CallbackContext obj) => OnMove?.Invoke(obj);
    private void HighlightStarted(InputAction.CallbackContext obj) => OnHighlightStarted?.Invoke(obj);
    private void HighlightEnded(InputAction.CallbackContext obj) => OnHighlightEnded?.Invoke(obj);
    private void Quicksave(InputAction.CallbackContext obj) => OnQuicksave?.Invoke(obj);
    private void Quickload(InputAction.CallbackContext obj) => OnQuickload?.Invoke(obj);


    private void SetPause(bool isPaused)
    {
        if (isPaused) inputActions.Player.Disable();
        else inputActions.Player.Enable();
    }

    public string GetBindingName(Bindings binding)
    {


        switch (binding)
        {

            case Bindings.MainMenu:
                return inputActions.System.MainMenu.bindings[0].ToDisplayString();

            case Bindings.GameConsole:
                return inputActions.System.GameConsole.bindings[0].ToDisplayString();

            case Bindings.Pause:
                return inputActions.System.Pause.bindings[0].ToDisplayString();

            case Bindings.Inventory:
                return inputActions.Player.Inventory.bindings[0].ToDisplayString();

            case Bindings.CharacterTab:
                return inputActions.Player.CharacterTab.bindings[0].ToDisplayString();

            case Bindings.Action:
                return inputActions.Player.Action.bindings[0].ToDisplayString();

            case Bindings.Move:
                return inputActions.Player.Move.bindings[0].ToDisplayString();

            case Bindings.Highlight:
                return inputActions.Player.Highlight.bindings[0].ToDisplayString();

            case Bindings.Quicksave:
                return inputActions.Player.QuickSave.bindings[0].ToDisplayString();

            case Bindings.Quickload:
                return inputActions.Player.QuickLoad.bindings[0].ToDisplayString();

        }

        return "n/d";

    }


    public void RebindBinding(Bindings binding, Action onActionRebound)
    {

        InputAction inputAction;

        switch (binding)
        {
            case Bindings.MainMenu:
                inputAction = inputActions.System.MainMenu;
                break;

            case Bindings.Inventory:
                inputAction = inputActions.Player.Inventory;
                break;

            case Bindings.CharacterTab:
                inputAction = inputActions.Player.CharacterTab;
                break;

            default:
            case Bindings.Action:
                inputAction = inputActions.Player.Action;
                break;

            case Bindings.Move:
                inputAction = inputActions.Player.Move;
                break;

            case Bindings.Highlight:
                inputAction = inputActions.Player.Highlight;
                break;

            case Bindings.Pause:
                inputAction = inputActions.System.Pause;
                break;

            case Bindings.Quicksave:
                inputAction = inputActions.Player.QuickSave;
                break;

            case Bindings.Quickload:
                inputAction = inputActions.Player.QuickLoad;
                break;
        }

        bool isMapActive = inputAction.actionMap.enabled;
        if (isMapActive) inputAction.actionMap.Disable();

        inputAction.PerformInteractiveRebinding(0).
            WithControlsExcluding("<keyboard>/backquote").
            WithControlsExcluding("<keyboard>/anykey").
            OnComplete(callback =>
        {
            if (isMapActive) inputAction.actionMap.Enable();
            callback.Dispose();
            onActionRebound();

            PlayerPrefs.SetString(playerBindings, inputActions.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();

        }).Start();
    }


    public void RestoreDefault(Action callback)
    {
        inputActions.RemoveAllBindingOverrides();
        PlayerPrefs.DeleteKey(playerBindings);

        if (callback != null) callback();
    }

}
