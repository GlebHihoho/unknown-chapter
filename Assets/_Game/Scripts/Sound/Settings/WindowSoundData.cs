using UnityEngine;

[CreateAssetMenu(fileName = "WindowSoundSettings", menuName = "Gameplay/Sounds/Window")]
public class WindowSoundData : ScriptableObject
{
    [SerializeField] AudioClip opened;
    public AudioClip Opened => opened;

    [SerializeField] AudioClip closed;
    public AudioClip Closed => closed;
}
