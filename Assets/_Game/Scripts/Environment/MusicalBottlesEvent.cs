using System.Collections;
using UI;
using Unity.Cinemachine;
using UnityEngine;

public class MusicalBottlesEvent : MonoBehaviour
{

    [SerializeField] AudioClip[] correctOrder;

    [SerializeField] AudioClip melodyShort;
    [SerializeField] AudioClip melodyFull;

    [SerializeField] CinemachineCamera bottlesCamera;

    WaitForSeconds delay = new WaitForSeconds(1.2f);

    WaitForSeconds shortMelody;
    WaitForSeconds longMelody;

    UIController uiContoller;

    int index = 0;

    void Start()
    {
        MusicalBottle.OnClipCalled += ClipCalled;

        shortMelody = new WaitForSeconds(melodyShort.length);
        longMelody = new WaitForSeconds(melodyFull.length);

        uiContoller = FindFirstObjectByType<UIController>();
    }

    private void OnDestroy() => MusicalBottle.OnClipCalled -= ClipCalled;


    private void ClipCalled(AudioClip clip)
    {
        SoundManager.instance.PlayEffect(clip);

        if (correctOrder[index] == clip)
        {
            index++;

            if (index == correctOrder.Length)
            {
                StartCoroutine(PlayMeloody(melodyFull, longMelody, true));

                index = 0;
            }
        }
        else
        {
            index = 0;
            StartCoroutine(PlayMeloody(melodyShort, shortMelody, false));
        }
    }

    IEnumerator PlayMeloody(AudioClip clip, WaitForSeconds melodyDelay, bool isWin)
    {
        
        SoundManager.instance.MuffleMusic();

        if (isWin) 
        {
            GameControls.instance.DisableAllControls();
            uiContoller.DisableUI();

            bottlesCamera.enabled = true; 
        }

        yield return delay;
        SoundManager.instance.PlayEffect(clip);

        yield return melodyDelay;
        SoundManager.instance.RestoreMusic();

        bottlesCamera.enabled = false;

        if (isWin)
        {
            GameControls.instance.ReenableAllControls();
            uiContoller.EnableUI();
        }
    }


}
