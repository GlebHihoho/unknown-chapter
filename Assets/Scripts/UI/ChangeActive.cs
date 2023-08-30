using UnityEngine;

public class ChangeActive : MonoBehaviour
{
    [SerializeField] private GameObject _activeGameObject;
    [SerializeField] private GameObject _nonActiveGameObject;

    public void OnClick()
    {
        _activeGameObject.SetActive(!_activeGameObject.activeSelf);
        _nonActiveGameObject.SetActive(!_nonActiveGameObject.activeSelf);
    }
}