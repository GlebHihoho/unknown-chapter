using UnityEngine;

[CreateAssetMenu(fileName = "MenuSoundSettings", menuName = "Gameplay/Sounds/Menu")]
public class MenuSoundData : ScriptableObject
{
    [SerializeField] AudioClip buttonClick;
    public AudioClip ButtonClick => buttonClick;

    [SerializeField] AudioClip arrows;
    public AudioClip Arrows => arrows;

    [SerializeField] AudioClip slider;
    public AudioClip Slider => slider;
}
