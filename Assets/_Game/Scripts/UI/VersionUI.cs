using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class VersionUI : MonoBehaviour
{

    TextMeshProUGUI label;

    void Start()
    {
        label = GetComponent<TextMeshProUGUI>();
        label.text = $"v {Application.version}";
    }

}
