using UnityEngine;

namespace Environment
{

    [CreateAssetMenu(fileName = "EnvironmentZone", menuName = "Gameplay/Environment Zone Data")]
    public class ZoneData : ScriptableObject
    {

        [SerializeField] string saveID;
        public string SaveID => saveID;

        [SerializeField] string label;
        public string Label => label;

        [SerializeField] AudioClip backgroundMusic;
        public AudioClip BackgroundMusic => backgroundMusic;


    }
}
