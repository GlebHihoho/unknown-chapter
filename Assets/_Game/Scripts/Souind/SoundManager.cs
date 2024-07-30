using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField] AudioSource ambientMusic1;
    [SerializeField] AudioSource ambientMusic2;

    [SerializeField, Range(0, 5)] float shuffleTime = 1.5f;
    float timer = 0;

    int activeAmbient = 1;
    AudioClip activeMusic;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Environment.ZoneManager.OnChangeAmbientMusic += ChangeAmbientMusic;

        ambientMusic1.volume = 1;
        ambientMusic2.volume = 0;
    }


    private void OnDestroy()
    {
        Environment.ZoneManager.OnChangeAmbientMusic -= ChangeAmbientMusic;
    }


    private void ChangeAmbientMusic(AudioClip clip)
    {

        if (activeMusic != clip)
        {
            activeMusic = clip;

            if (activeAmbient == 1)
            {
                activeAmbient = 2;
                ambientMusic2.clip = clip;
                ambientMusic2.Play();
            }
            else if (activeAmbient == 2)
            {
                activeAmbient = 1;
                ambientMusic1.clip = clip;
                ambientMusic1.Play();
            }

            timer = shuffleTime;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;

            if (activeAmbient == 1)
            {
                ambientMusic1.volume = Mathf.InverseLerp(shuffleTime, 0, timer);
                ambientMusic2.volume = Mathf.InverseLerp(0, shuffleTime, timer);

                if (timer <= 0) ambientMusic2.Stop();
            }
            else if (activeAmbient == 2)
            {
                ambientMusic2.volume = Mathf.InverseLerp(shuffleTime, 0, timer);
                ambientMusic1.volume = Mathf.InverseLerp(0, shuffleTime, timer);

                if(timer <= 0) ambientMusic1.Stop(); 
            }
        }
    }
}
