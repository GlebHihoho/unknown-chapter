using UnityEngine;

public class TriggerVisuals : MonoBehaviour
{
    MeshRenderer render;

    private void Awake()
    {
        render = GetComponent<MeshRenderer>();
        render.enabled = false;
    }

    private void Start() => GameConsole.instance.OnToggleTriggersView += ToggleTriggerVisibility;
    private void OnDestroy() => GameConsole.instance.OnToggleTriggersView -= ToggleTriggerVisibility;
    private void ToggleTriggerVisibility() => render.enabled = !render.enabled;

}
