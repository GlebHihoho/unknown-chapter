using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameControls : MonoBehaviour
{

    PlayerInputActions inputActions;

    public static GameControls instance;

    public event Action<InputAction.CallbackContext> OnMainMenu;
    public event Action<InputAction.CallbackContext> OnGameConsole;

    public event Action<InputAction.CallbackContext> OnInventory;
    public event Action<InputAction.CallbackContext> OnCharacterTab;
    public event Action<InputAction.CallbackContext> OnFire;
    public event Action<InputAction.CallbackContext> OnAltFire;
    public event Action<InputAction.CallbackContext> OnHighlightStarted;
    public event Action<InputAction.CallbackContext> OnHighlightEnded;
    public event Action<InputAction.CallbackContext> OnPause;
    public event Action<InputAction.CallbackContext> OnQuicksave;
    public event Action<InputAction.CallbackContext> OnQuickload;



    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);

        inputActions = new();
        inputActions.System.Enable();
        inputActions.Player.Enable();

        inputActions.System.MainMenu.performed += MainMenu;
        inputActions.System.GameConsole.performed += GameConsole;
        inputActions.System.Pause.performed += PauseGame;

        inputActions.Player.Inventory.performed += Inventory;
        inputActions.Player.CharacterTab.performed += CharacterTab;
        inputActions.Player.Fire.performed += Fire;
        inputActions.Player.AltFire.performed += AltFire;
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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void MainMenu(InputAction.CallbackContext obj) => OnMainMenu?.Invoke(obj);
    private void GameConsole(InputAction.CallbackContext obj) => OnGameConsole?.Invoke(obj);
    private void PauseGame(InputAction.CallbackContext obj) => OnPause?.Invoke(obj);

    private void Inventory(InputAction.CallbackContext obj) => OnInventory?.Invoke(obj);
    private void CharacterTab(InputAction.CallbackContext obj) => OnCharacterTab?.Invoke(obj);
    private void Fire(InputAction.CallbackContext obj) => OnFire?.Invoke(obj);
    private void AltFire(InputAction.CallbackContext obj) => OnAltFire?.Invoke(obj);
    private void HighlightStarted(InputAction.CallbackContext obj) => OnHighlightStarted?.Invoke(obj);
    private void HighlightEnded(InputAction.CallbackContext obj) => OnHighlightEnded?.Invoke(obj);
    private void Quicksave(InputAction.CallbackContext obj) => OnQuicksave?.Invoke(obj);
    private void Quickload(InputAction.CallbackContext obj) => OnQuickload?.Invoke(obj);


    private void SetPause(bool isPaused)
    {
        if (isPaused) inputActions.Player.Disable();
        else inputActions.Player.Enable();
    }
}
