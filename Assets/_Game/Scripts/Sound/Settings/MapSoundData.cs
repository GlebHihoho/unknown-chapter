using UnityEngine;

[CreateAssetMenu(fileName ="MapSoundSettins", menuName = "Gameplay/Sounds/Map")]
public class MapSoundData : ScriptableObject
{
    [SerializeField] AudioClip newLocation;
    public AudioClip NewLocation => newLocation;
}
