using System.Collections;
using UnityEngine;

public class MusicalBottlesEvent : MonoBehaviour
{

    [SerializeField] AudioClip[] correctOrder;

    [SerializeField] AudioClip melodyShort;
    [SerializeField] AudioClip melodyFull;

    WaitForSeconds delay = new WaitForSeconds(1.2f);

    WaitForSeconds shortMelody;
    WaitForSeconds longMelody;

    int index = 0;

    void Start()
    {
        MusicalBottle.OnClipCalled += ClipCalled;

        shortMelody = new WaitForSeconds(melodyShort.length);
        longMelody = new WaitForSeconds(melodyFull.length);
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
                StartCoroutine(PlayMeloody(melodyFull, longMelody));

                index = 0;
            }
        }
        else
        {
            index = 0;
            StartCoroutine(PlayMeloody(melodyShort, shortMelody));
        }
    }

    IEnumerator PlayMeloody(AudioClip clip, WaitForSeconds melodyDelay)
    {
        
        SoundManager.instance.MuffleMusic();

        yield return delay;
        SoundManager.instance.PlayEffect(clip);

        yield return melodyDelay;
        SoundManager.instance.RestoreMusic();
    }


}
