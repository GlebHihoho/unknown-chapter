using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class GameControls : MonoBehaviour
{

    const string playerBindings = "InputBindings";

    PlayerInputActions inputActions;

    public static GameControls instance;

    public event Action OnMainMenu;

    public event Action OnSkipIntroStarted;
    public event Action OnSkipIntroEnded;

    public event Action OnInventory;
    public event Action OnCharacterTab;
    public event Action OnAction;
    public event Action OnMove;

    public event Action OnMoveStarted;
    public event Action OnMoveEnded;

    public event Action OnHighlightStarted;
    public event Action OnHighlightEnded;
    public event Action OnPause;
    public event Action OnQuicksave;
    public event Action OnQuickload;
    public event Action OnMap;
    public event Action OnJournal;

    public event Action OnCameraRotateStarted;
    public event Action OnCameraRotateEnded;

    public enum Bindings { MainMenu, Pause, Inventory, CharacterTab, Action, Move, Highlight, Quicksave, Quickload, Map, Journal, CameraRotation };



    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);

        inputActions = new();

        if (PlayerPrefs.HasKey(playerBindings)) inputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(playerBindings));


        inputActions.System.Enable();
        inputActions.Player.Enable();

        inputActions.System.MainMenu.performed += MainMenu;

        inputActions.System.SkipIntro.started += SkipIntroStarted;
        inputActions.System.SkipIntro.canceled += SkipIntroEnded;

        inputActions.System.Pause.performed += PauseGame;

        inputActions.Player.Inventory.performed += Inventory;
        inputActions.Player.CharacterTab.performed += CharacterTab;
        inputActions.Player.Action.performed += Action;

        inputActions.Player.Move.performed += Move;

        inputActions.Player.Move.started += MoveStarted;
        inputActions.Player.Move.canceled += MoveEnded;

        inputActions.Player.Highlight.started += HighlightStarted;
        inputActions.Player.Highlight.canceled += HighlightEnded;
        inputActions.Player.QuickSave.performed += Quicksave;
        inputActions.Player.QuickLoad.performed += Quickload;
        inputActions.Player.Map.performed += Map;
        inputActions.Player.Journal.performed += Journal;

        inputActions.Player.CameraRotate.started += CameraRotateStarted;
        inputActions.Player.CameraRotate.canceled += CameraRotateEnded;

    }


    private void OnDestroy()
    {
        if (inputActions != null) 
        {
            inputActions.System.Disable();
            inputActions.Player.Disable(); 
        }
    }

    private void OnEnable() => Pause.OnDisableKeys += SetPause;
    private void OnDisable() => Pause.OnDisableKeys -= SetPause;


    private void MainMenu(InputAction.CallbackContext obj) => OnMainMenu?.Invoke();


    private void SkipIntroStarted(InputAction.CallbackContext obj) => OnSkipIntroStarted?.Invoke();
    private void SkipIntroEnded(InputAction.CallbackContext obj) => OnSkipIntroEnded?.Invoke();


    private void PauseGame(InputAction.CallbackContext obj) => OnPause?.Invoke();

    private void Inventory(InputAction.CallbackContext obj) => OnInventory?.Invoke();
    private void CharacterTab(InputAction.CallbackContext obj) => OnCharacterTab?.Invoke();
    private void Action(InputAction.CallbackContext obj) => OnAction?.Invoke();
    private void Move(InputAction.CallbackContext obj) => OnMove?.Invoke();

    private void MoveStarted(InputAction.CallbackContext obj) => OnMoveStarted?.Invoke();
    private void MoveEnded(InputAction.CallbackContext obj) => OnMoveEnded?.Invoke();


    private void HighlightStarted(InputAction.CallbackContext obj) => OnHighlightStarted?.Invoke();
    private void HighlightEnded(InputAction.CallbackContext obj) => OnHighlightEnded?.Invoke();
    private void Quicksave(InputAction.CallbackContext obj) => OnQuicksave?.Invoke();
    private void Quickload(InputAction.CallbackContext obj) => OnQuickload?.Invoke();
    private void Map(InputAction.CallbackContext obj) => OnMap?.Invoke();
    private void Journal(InputAction.CallbackContext obj) => OnJournal.Invoke();

    private void CameraRotateStarted(InputAction.CallbackContext obj) => OnCameraRotateStarted?.Invoke();
    private void CameraRotateEnded(InputAction.CallbackContext obj) => OnCameraRotateEnded?.Invoke();


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

            case Bindings.Map:
                return inputActions.Player.Map.bindings[0].ToDisplayString();

            case Bindings.Journal:
                return inputActions.Player.Journal.bindings[0].ToDisplayString();

            case Bindings.CameraRotation:
                return inputActions.Player.CameraRotate.bindings[0].ToDisplayString();

        }

        return "n/d";

    }


    public void RebindBinding(Bindings binding, Action onActionRebound, Action onReboundCanceled)
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

            case Bindings.Map:
                inputAction = inputActions.Player.Map;
                break;

            case Bindings.Journal:
                inputAction = inputActions.Player.Journal;
                break;

            case Bindings.CameraRotation:
                inputAction = inputActions.Player.CameraRotate;
                break;
        }


        inputActions.System.Disable(); // Always disabled for use Esc for cancel

        bool isMapActive = inputAction.actionMap.enabled;
        if (isMapActive) inputAction.actionMap.Disable();

        inputAction.PerformInteractiveRebinding(0).
            WithControlsExcluding("<keyboard>/backquote"). // Reserved for the game console
            WithControlsExcluding("<keyboard>/anykey").
            WithCancelingThrough("<keyboard>/escape").
            OnComplete(callback =>
        {
            if (isMapActive) inputAction.actionMap.Enable();
            inputActions.System.Enable();

            callback.Dispose();
            onActionRebound();

            PlayerPrefs.SetString(playerBindings, inputActions.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();

        }).OnCancel(callback =>
        {
            callback.Dispose();
            onReboundCanceled();

            if (isMapActive) inputAction.actionMap.Enable();
            StartCoroutine(ReenableSystemActions());

        }).Start();
    }

    IEnumerator ReenableSystemActions()
    {
        yield return null;
        inputActions.System.Enable(); 
    }


    public void RestoreDefault(Action callback)
    {
        inputActions.RemoveAllBindingOverrides();
        PlayerPrefs.DeleteKey(playerBindings);

        if (callback != null) callback();
    }

}
