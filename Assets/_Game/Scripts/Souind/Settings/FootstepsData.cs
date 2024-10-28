using UnityEngine;

[CreateAssetMenu(fileName = "FootstepsData", menuName = "Gameplay/Sounds/Footsteps")]
public class FootstepsData : ScriptableObject
{
    [SerializeField] AudioClip defaultFootsteps;
    public AudioClip DefaultFootsteps => defaultFootsteps;
}
