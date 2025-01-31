using UI;
using UnityEngine;
using UnityEngine.Events;

public class MenuTrigger : MonoBehaviour
{

    public UnityEvent OnShowMenu;
    public UnityEvent OnHideMenu;

    bool isShown = false;

    bool isEnabled = true;


    private void Start()
    {
        UIController.OnMainMenu += MainMenu;
        MainMenuToggle.OnHideMenu += ResetShown;
        UIController.OnEnableUI += EnableUI;
    }



    private void OnDestroy()
    {
        UIController.OnMainMenu -= MainMenu;
        MainMenuToggle.OnHideMenu -= ResetShown;
        UIController.OnEnableUI -= EnableUI;
    }


    private void EnableUI(bool isEnabled) => this.isEnabled = isEnabled;


    public void MainMenu()
    {
        if (!isEnabled) return;

        isShown = !isShown;

        if (isShown) OnShowMenu.Invoke();
        else OnHideMenu.Invoke();
    }

    private void ResetShown() => isShown = false;

}
