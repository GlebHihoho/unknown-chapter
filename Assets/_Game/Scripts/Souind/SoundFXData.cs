using UnityEngine;

[CreateAssetMenu(fileName = "SoundFXData", menuName = "Gameplay/Sounds Settings")]
public class SoundFXData : ScriptableObject
{

    [SerializeField] AudioClip buttonClick;
    public AudioClip ButtonClick => buttonClick;
    
}
