using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuTrigger : MonoBehaviour
{

    public UnityEvent OnShowMenu;
    public UnityEvent OnHideMenu;

    bool isShown = false;

    private void Awake()
    {
        PlayerInputActions playerInputActions = new();
        playerInputActions.Player.Enable();
        playerInputActions.Player.MainMenu.performed += MainMenu;

    }


    private void MainMenu(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        isShown = !isShown;

        if (isShown) OnShowMenu.Invoke();
        else OnHideMenu.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isShown = !isShown;

            if (isShown) OnShowMenu.Invoke();
            else OnHideMenu.Invoke();
        }
        */
    }
}
