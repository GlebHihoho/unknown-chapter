using UI;
using UnityEngine;
using UnityEngine.Events;

public class MenuTrigger : MonoBehaviour
{

    public UnityEvent OnShowMenu;
    public UnityEvent OnHideMenu;

    bool isShown = false;


    private void Start() => UIController.OnMainMenu += MainMenu;
    private void OnDestroy() => UIController.OnMainMenu -= MainMenu;

    public void MainMenu()
    {
        isShown = !isShown;

        if (isShown) OnShowMenu.Invoke();
        else OnHideMenu.Invoke();
    }

}
