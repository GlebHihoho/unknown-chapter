using UnityEngine;

[CreateAssetMenu(fileName = "JournalSoundSettings", menuName = "Gameplay/Sounds/Journal")]
public class JournalSoundData : ScriptableObject
{
    [SerializeField] AudioClip newNote;
    public AudioClip NewNote => newNote;
}
