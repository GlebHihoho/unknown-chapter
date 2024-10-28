using UnityEngine;

[CreateAssetMenu(fileName ="ItemSoundSettings", menuName ="Gameplay/Sounds/Item")]
public class ItemSoundData : ScriptableObject
{
    [SerializeField] AudioClip recieved;
    public AudioClip Receieved => recieved;

    [SerializeField] AudioClip selected;
    public AudioClip Selected => selected;

    [SerializeField] AudioClip deleted;
    public AudioClip Deleted => deleted;
}
