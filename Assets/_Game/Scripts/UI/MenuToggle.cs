using UnityEngine;

public class MenuToggle : MonoBehaviour
{
    [SerializeField] GameObject mainPanel;

    GameObject currentPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPanel = mainPanel;
    }


    private void OnEnable()
    {
        if (currentPanel != null && currentPanel != mainPanel)
        {
            currentPanel.SetActive(false);
            currentPanel = mainPanel;
        }
        mainPanel.SetActive(true);
    }


    public void ChangePanel(GameObject newPanel)
    {
        currentPanel.SetActive(false);
        newPanel.SetActive(true);

        currentPanel = newPanel;
    }
}
