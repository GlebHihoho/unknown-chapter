using UnityEngine;

public class ToggleObject : MonoBehaviour
{

    [SerializeField] GameObject target;

    private void Start() => target.SetActive(false);

    public void Toggle(bool isToggle) => target.SetActive(isToggle);


}
