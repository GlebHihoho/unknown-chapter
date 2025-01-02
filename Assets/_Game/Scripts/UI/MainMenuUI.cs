using UnityEngine;
using System.Collections.Generic;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] GameControls gameControls;
    [SerializeField] GameObject mainPanel;

    GameObject activePanel;

    Stack<GameObject> openedPanels = new();

    bool isCalled = false;

    private void Awake() => activePanel = mainPanel;


    private void OnEnable()
    {

        if (gameControls == null) gameControls = GameControls.instance;

        gameControls.OnMainMenu += Return;

        openedPanels.Clear();
        if (activePanel != null && activePanel != mainPanel)
        {
            ToggleActivePanel(mainPanel);
        }
    }


    private void OnDisable()
    {
        gameControls.OnMainMenu -= Return;
        isCalled = false;
    }

    public void OpenMainMenu()
    {
        isCalled = true;
        gameObject.SetActive(true);
    }


    public void SelectMenu(GameObject newPanel)
    {
        openedPanels.Push(activePanel);
        ToggleActivePanel(newPanel);
    }


    public void Return()
    {
        if (openedPanels.Count > 0) ToggleActivePanel(openedPanels.Pop());
        else if (isCalled) gameObject.SetActive(false);
    }


    private void ToggleActivePanel(GameObject newPanel)
    {
        activePanel.SetActive(false);
        activePanel = newPanel;
        activePanel.SetActive(true);
    }


}
