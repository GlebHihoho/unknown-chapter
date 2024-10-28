using UnityEngine;

public class WindowSound : MonoBehaviour
{

    [SerializeField] WindowSoundData sounds;

    bool isFirstDisable = true;
    [SerializeField] bool ignoreFirstUse = true;


    private void OnEnable()
    {
        if((!isFirstDisable || !ignoreFirstUse) && SoundManager.instance != null) 
            SoundManager.instance.PlayEffect(sounds.Opened);
    }

    private void OnDisable()
    {
        if ((!isFirstDisable || !ignoreFirstUse) && SoundManager.instance !=null) 
            SoundManager.instance.PlayEffect(sounds.Closed);

        isFirstDisable = false;
    }
}
