using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class InventoryExitButton : MonoBehaviour
{
    [SerializeField] private GameObject _backGround;

    public void OnClick()
    {
        _backGround.SetActive(!_backGround.activeSelf);
    }
}
