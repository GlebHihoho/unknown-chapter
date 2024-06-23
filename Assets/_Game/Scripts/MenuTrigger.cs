using UnityEngine;
using UnityEngine.Events;

public class MenuTrigger : MonoBehaviour
{

    public UnityEvent OnShowMenu;
    public UnityEvent OnHideMenu;

    bool isShown = false;


    private void Start() => GameControls.instance.OnMainMenu += MainMenu;
    private void OnDestroy() => GameControls.instance.OnMainMenu -= MainMenu;

    private void MainMenu(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        isShown = !isShown;

        if (isShown) OnShowMenu.Invoke();
        else OnHideMenu.Invoke();
    }

}
