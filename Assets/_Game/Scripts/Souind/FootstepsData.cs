using UnityEngine;

[CreateAssetMenu(fileName = "FootstepsData", menuName = "Gameplay/FootstepsData")]
public class FootstepsData : ScriptableObject
{
    [SerializeField] AudioClip defaultFootsteps;
    public AudioClip DefaultFootsteps => defaultFootsteps;
}
