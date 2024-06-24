//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.8.2
//     from Assets/_Game/InputSystem/PlayerInputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine;

public partial class @PlayerInputActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""edce0809-cdb2-46ac-80c3-c07d2e93571c"",
            ""actions"": [
                {
                    ""name"": ""Inventory"",
                    ""type"": ""Button"",
                    ""id"": ""25c75b05-edc2-4b9c-94e9-81874a384985"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CharacterTab"",
                    ""type"": ""Button"",
                    ""id"": ""a5829008-6ee8-49e0-8227-13b9b21a401a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Action"",
                    ""type"": ""Button"",
                    ""id"": ""7ecfaf14-fa1c-426a-82aa-ee1bf2c4d020"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""4917f3b8-fb08-429b-a19c-91e8fa1e8931"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Highlight"",
                    ""type"": ""Button"",
                    ""id"": ""985b9ab9-e947-4430-9724-3ac71ca1f741"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""QuickSave"",
                    ""type"": ""Button"",
                    ""id"": ""69a8f0c7-5a65-4e58-a520-e163759d4bf1"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""QuickLoad"",
                    ""type"": ""Button"",
                    ""id"": ""1619e850-2d69-4b30-9a13-9cfb37e8f9f9"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""211f83ce-0f7a-4509-bd7b-5199a4672c25"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8e11f9f2-ad81-400e-9524-bebb6da5fa0c"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CharacterTab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ee00c971-c25b-4ad4-9fab-0b9ef5788acd"",
                    ""path"": ""<Pointer>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""65ea271f-a42e-453b-bb63-1b1a51042bef"",
                    ""path"": ""<Pointer>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e5add0dc-f60b-43bf-8a3c-4bab4570e9d0"",
                    ""path"": ""<Keyboard>/alt"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Highlight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2e9d0ad7-cbed-459f-9646-bec40c6dcb92"",
                    ""path"": ""<Keyboard>/f5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""QuickSave"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dd68dd30-2644-41ef-b96c-1387612ab226"",
                    ""path"": ""<Keyboard>/f6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""QuickLoad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""System"",
            ""id"": ""b7518480-ed2d-4333-a3fb-85899b8f4782"",
            ""actions"": [
                {
                    ""name"": ""MainMenu"",
                    ""type"": ""Button"",
                    ""id"": ""48338b57-03e2-4cb5-a1e8-429f709f5bcd"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""3f79fd00-6392-4e2f-b60f-5ce317bf3710"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""GameConsole"",
                    ""type"": ""Button"",
                    ""id"": ""ed07ad62-d25b-457f-8baa-6b14c07a0f2e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""55e2813a-9abd-4d19-b02d-862fcff01d2f"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MainMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fcb31a38-13c2-4f72-888c-112fde03ffcc"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""acef3e53-e286-4f81-9c44-0c00e3a69477"",
                    ""path"": ""<Keyboard>/backquote"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GameConsole"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Inventory = m_Player.FindAction("Inventory", throwIfNotFound: true);
        m_Player_CharacterTab = m_Player.FindAction("CharacterTab", throwIfNotFound: true);
        m_Player_Action = m_Player.FindAction("Action", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Highlight = m_Player.FindAction("Highlight", throwIfNotFound: true);
        m_Player_QuickSave = m_Player.FindAction("QuickSave", throwIfNotFound: true);
        m_Player_QuickLoad = m_Player.FindAction("QuickLoad", throwIfNotFound: true);
        // System
        m_System = asset.FindActionMap("System", throwIfNotFound: true);
        m_System_MainMenu = m_System.FindAction("MainMenu", throwIfNotFound: true);
        m_System_Pause = m_System.FindAction("Pause", throwIfNotFound: true);
        m_System_GameConsole = m_System.FindAction("GameConsole", throwIfNotFound: true);
    }

    ~@PlayerInputActions()
    {
        Debug.Assert(!m_Player.enabled, "This will cause a leak and performance issues, PlayerInputActions.Player.Disable() has not been called.");
        Debug.Assert(!m_System.enabled, "This will cause a leak and performance issues, PlayerInputActions.System.Disable() has not been called.");
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_Inventory;
    private readonly InputAction m_Player_CharacterTab;
    private readonly InputAction m_Player_Action;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Highlight;
    private readonly InputAction m_Player_QuickSave;
    private readonly InputAction m_Player_QuickLoad;
    public struct PlayerActions
    {
        private @PlayerInputActions m_Wrapper;
        public PlayerActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Inventory => m_Wrapper.m_Player_Inventory;
        public InputAction @CharacterTab => m_Wrapper.m_Player_CharacterTab;
        public InputAction @Action => m_Wrapper.m_Player_Action;
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Highlight => m_Wrapper.m_Player_Highlight;
        public InputAction @QuickSave => m_Wrapper.m_Player_QuickSave;
        public InputAction @QuickLoad => m_Wrapper.m_Player_QuickLoad;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @Inventory.started += instance.OnInventory;
            @Inventory.performed += instance.OnInventory;
            @Inventory.canceled += instance.OnInventory;
            @CharacterTab.started += instance.OnCharacterTab;
            @CharacterTab.performed += instance.OnCharacterTab;
            @CharacterTab.canceled += instance.OnCharacterTab;
            @Action.started += instance.OnAction;
            @Action.performed += instance.OnAction;
            @Action.canceled += instance.OnAction;
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Highlight.started += instance.OnHighlight;
            @Highlight.performed += instance.OnHighlight;
            @Highlight.canceled += instance.OnHighlight;
            @QuickSave.started += instance.OnQuickSave;
            @QuickSave.performed += instance.OnQuickSave;
            @QuickSave.canceled += instance.OnQuickSave;
            @QuickLoad.started += instance.OnQuickLoad;
            @QuickLoad.performed += instance.OnQuickLoad;
            @QuickLoad.canceled += instance.OnQuickLoad;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @Inventory.started -= instance.OnInventory;
            @Inventory.performed -= instance.OnInventory;
            @Inventory.canceled -= instance.OnInventory;
            @CharacterTab.started -= instance.OnCharacterTab;
            @CharacterTab.performed -= instance.OnCharacterTab;
            @CharacterTab.canceled -= instance.OnCharacterTab;
            @Action.started -= instance.OnAction;
            @Action.performed -= instance.OnAction;
            @Action.canceled -= instance.OnAction;
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Highlight.started -= instance.OnHighlight;
            @Highlight.performed -= instance.OnHighlight;
            @Highlight.canceled -= instance.OnHighlight;
            @QuickSave.started -= instance.OnQuickSave;
            @QuickSave.performed -= instance.OnQuickSave;
            @QuickSave.canceled -= instance.OnQuickSave;
            @QuickLoad.started -= instance.OnQuickLoad;
            @QuickLoad.performed -= instance.OnQuickLoad;
            @QuickLoad.canceled -= instance.OnQuickLoad;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // System
    private readonly InputActionMap m_System;
    private List<ISystemActions> m_SystemActionsCallbackInterfaces = new List<ISystemActions>();
    private readonly InputAction m_System_MainMenu;
    private readonly InputAction m_System_Pause;
    private readonly InputAction m_System_GameConsole;
    public struct SystemActions
    {
        private @PlayerInputActions m_Wrapper;
        public SystemActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @MainMenu => m_Wrapper.m_System_MainMenu;
        public InputAction @Pause => m_Wrapper.m_System_Pause;
        public InputAction @GameConsole => m_Wrapper.m_System_GameConsole;
        public InputActionMap Get() { return m_Wrapper.m_System; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SystemActions set) { return set.Get(); }
        public void AddCallbacks(ISystemActions instance)
        {
            if (instance == null || m_Wrapper.m_SystemActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_SystemActionsCallbackInterfaces.Add(instance);
            @MainMenu.started += instance.OnMainMenu;
            @MainMenu.performed += instance.OnMainMenu;
            @MainMenu.canceled += instance.OnMainMenu;
            @Pause.started += instance.OnPause;
            @Pause.performed += instance.OnPause;
            @Pause.canceled += instance.OnPause;
            @GameConsole.started += instance.OnGameConsole;
            @GameConsole.performed += instance.OnGameConsole;
            @GameConsole.canceled += instance.OnGameConsole;
        }

        private void UnregisterCallbacks(ISystemActions instance)
        {
            @MainMenu.started -= instance.OnMainMenu;
            @MainMenu.performed -= instance.OnMainMenu;
            @MainMenu.canceled -= instance.OnMainMenu;
            @Pause.started -= instance.OnPause;
            @Pause.performed -= instance.OnPause;
            @Pause.canceled -= instance.OnPause;
            @GameConsole.started -= instance.OnGameConsole;
            @GameConsole.performed -= instance.OnGameConsole;
            @GameConsole.canceled -= instance.OnGameConsole;
        }

        public void RemoveCallbacks(ISystemActions instance)
        {
            if (m_Wrapper.m_SystemActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ISystemActions instance)
        {
            foreach (var item in m_Wrapper.m_SystemActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_SystemActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public SystemActions @System => new SystemActions(this);
    public interface IPlayerActions
    {
        void OnInventory(InputAction.CallbackContext context);
        void OnCharacterTab(InputAction.CallbackContext context);
        void OnAction(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnHighlight(InputAction.CallbackContext context);
        void OnQuickSave(InputAction.CallbackContext context);
        void OnQuickLoad(InputAction.CallbackContext context);
    }
    public interface ISystemActions
    {
        void OnMainMenu(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnGameConsole(InputAction.CallbackContext context);
    }
}
