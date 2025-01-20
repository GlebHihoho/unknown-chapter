using TMPro;
using UnityEngine;

public class RebindPrompt : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI actionLabel;

    public void Show(string actionName)
    {
        actionLabel.text = actionName;
        gameObject.SetActive(true);
    }

    public void Hide() 
    { 
        gameObject.SetActive(false);
    }
}
