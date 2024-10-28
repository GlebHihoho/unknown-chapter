using UnityEngine;

public class WindowSound : MonoBehaviour
{

    [SerializeField] WindowSoundData sounds;

    bool isFirstDisable = true;
    [SerializeField] bool ignoreFirstDisable = true;


    private void OnEnable() => SoundManager.instance.PlayEffect(sounds.Opened);
    private void OnDisable()
    {
        if (!isFirstDisable || !ignoreFirstDisable) SoundManager.instance.PlayEffect(sounds.Closed);

        isFirstDisable = false;
    }
}
