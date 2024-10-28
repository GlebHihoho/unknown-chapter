using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSound : MonoBehaviour
{

    Button button;

    enum ButtonType {Standard, Arrow}
    [SerializeField] ButtonType type = ButtonType.Standard;

    [SerializeField] MenuSoundData sounds;


    private void Awake() => button = GetComponent<Button>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() => button.onClick.AddListener(PlaySound);

    private void OnDestroy() => button.onClick.RemoveListener(PlaySound);

    private void PlaySound()
    {

        AudioClip clip;

        switch (type)
        {
            default:
            case ButtonType.Standard:
            clip = sounds.ButtonClick;
                break;

            case ButtonType.Arrow:
            clip = sounds.Arrows;
                break;

        }

        SoundManager.instance.PlayEffect(clip);
    }
}
