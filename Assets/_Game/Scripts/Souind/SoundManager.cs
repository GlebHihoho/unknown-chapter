using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;

    [SerializeField] AudioSource ambientMusic1;
    [SerializeField] AudioSource ambientMusic2;

    [SerializeField] AudioSource soundEffects;

    const float defaultVolume = 0.7f;

    [Space]
    [SerializeField] AudioClip mainTheme;

    [SerializeField, Range(0, 1)] float generalVolume = defaultVolume;
    public float GeneralVolume => generalVolume;

    [SerializeField, Range(0, 1)] float musicVolume = defaultVolume;
    public float MusicVolume => musicVolume;

    [SerializeField, Range(0, 1)] float effectsVolume = defaultVolume;
    public float EffectsVolume => effectsVolume;

    [SerializeField, Range(0, 5)] float shuffleTime = 1.5f;
    float timer = 0;

    int activeAmbient = 1;
    AudioClip activeMusic;


    const string generalVolumeString = "GeneralVolume";
    const string musicVolumeString = "MusicVolume";
    const string effectsVolumeString = "EffectsVolume";


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        generalVolume = PlayerPrefs.GetFloat(generalVolumeString, defaultVolume);
        musicVolume = PlayerPrefs.GetFloat (musicVolumeString, defaultVolume);
        effectsVolume = PlayerPrefs.GetFloat(effectsVolumeString, defaultVolume);


        Environment.ZoneManager.OnChangeAmbientMusic += ChangeAmbientMusic;

        ambientMusic1.volume = 1;
        ambientMusic2.volume = 0;

        if (mainTheme != null) ChangeAmbientMusic(mainTheme);

        SoundSettingsUI.OnGeneralVolumeChange += ChangeGeneralVolume;
        SoundSettingsUI.OnMusicVolumeChange += ChangeMusicVolume;
        SoundSettingsUI.OnEffectsVolumeChange += ChangeEfectsVolume;

        SoundSettingsUI.OnCloseSettings += SaveSettings;
    }



    private void OnDestroy()
    {
        Environment.ZoneManager.OnChangeAmbientMusic -= ChangeAmbientMusic;

        SoundSettingsUI.OnGeneralVolumeChange -= ChangeGeneralVolume;
        SoundSettingsUI.OnMusicVolumeChange -= ChangeMusicVolume;
        SoundSettingsUI.OnEffectsVolumeChange -= ChangeEfectsVolume;

        SoundSettingsUI.OnCloseSettings -= SaveSettings;
    }


    private void ChangeGeneralVolume(float volume) => generalVolume = volume;
    private void ChangeMusicVolume(float volume) => musicVolume = volume;
    private void ChangeEfectsVolume(float volume) => effectsVolume = volume;


    private void SaveSettings()
    {
        PlayerPrefs.SetFloat(generalVolumeString, generalVolume);
        PlayerPrefs.SetFloat (musicVolumeString, musicVolume);
        PlayerPrefs.SetFloat(effectsVolumeString, effectsVolume);
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
