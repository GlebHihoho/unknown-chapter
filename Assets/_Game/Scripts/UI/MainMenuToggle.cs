using System;
using UnityEngine;

public class MainMenuToggle : MonoBehaviour
{

    public static event Action OnHideMenu;

    private void OnDisable() => OnHideMenu?.Invoke();
}
