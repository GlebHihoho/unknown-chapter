using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSound : MonoBehaviour
{

    Button button;


    public static event Action OnClick;

    private void Awake() => button = GetComponent<Button>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() => button.onClick.AddListener(PlaySound);

    private void OnDestroy() => button.onClick.RemoveListener(PlaySound);

    private void PlaySound() => OnClick?.Invoke();

}
