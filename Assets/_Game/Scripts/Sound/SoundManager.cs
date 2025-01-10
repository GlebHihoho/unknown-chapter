using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;

    [SerializeField] AudioSource ambientMusic1;
    [SerializeField] AudioSource ambientMusic2;

    [SerializeField] AudioSource soundEffects;
    [SerializeField] AudioSource soundEffectsContinuous;

    [SerializeField] AudioMixer mixer;


    const float defaultVolume = 0.7f;

    [Space]
    [SerializeField] AudioClip mainTheme;

    [SerializeField, Range(0, 1)] float masterVolume = defaultVolume;
    public float MasterVolume => masterVolume;

    [SerializeField, Range(0, 1)] float musicVolume = defaultVolume;
    public float MusicVolume => musicVolume;

    [SerializeField, Range(0, 1)] float effectsVolume = defaultVolume;
    public float EffectsVolume => effectsVolume;

    [SerializeField, Range(0, 5)] float shuffleTime = 1.5f;
    float timer = 0;

    int activeAmbient = 1;
    AudioClip activeMusic;


    const string masterVolumeString = "GeneralVolume";
    const string musicVolumeString = "MusicVolume";
    const string effectsVolumeString = "EffectsVolume";

    const string masterVolumeMixer = "MasterVolume";
    const string musicVolumeMixer = "MusicVolume";
    const string effectsVolumeMixer = "EffectsVolume";


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

        masterVolume = PlayerPrefs.GetFloat(masterVolumeString, defaultVolume);
        musicVolume = PlayerPrefs.GetFloat (musicVolumeString, defaultVolume);
        effectsVolume = PlayerPrefs.GetFloat(effectsVolumeString, defaultVolume);

        mixer.SetFloat(masterVolumeMixer, MixerVolume(masterVolume));
        mixer.SetFloat(musicVolumeMixer, MixerVolume(musicVolume));
        mixer.SetFloat(effectsVolumeMixer, MixerVolume(effectsVolume));
        


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


    private void ChangeGeneralVolume(float volume) => ChangeVolume(volume, out masterVolume, masterVolumeMixer);
    private void ChangeMusicVolume(float volume) => ChangeVolume(volume, out musicVolume, musicVolumeMixer);
    private void ChangeEfectsVolume(float volume) => ChangeVolume(volume, out effectsVolume, effectsVolumeMixer);


    private void ChangeVolume(float newVolume, out float currVolume, string mixerGroup)
    {
        currVolume = newVolume;
        mixer.SetFloat(mixerGroup, MixerVolume(newVolume));
    }

    private float MixerVolume(float volume) => Mathf.Log10(volume) * 20;

    private void SaveSettings()
    {
        PlayerPrefs.SetFloat(masterVolumeString, masterVolume);
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


    public void PlayEffect(AudioClip clip) => soundEffects.PlayOneShot(clip);

    public void MuffleMusic()
    {
        mixer.SetFloat(musicVolumeMixer, MixerVolume(0.001f));
    }

    public void RestoreMusic()
    {
        mixer.SetFloat(musicVolumeMixer, MixerVolume(musicVolume));
    }


    public void PlayContinuousEffect(AudioClip clip)
    {
        if (soundEffectsContinuous.clip != clip) soundEffectsContinuous.clip = clip;
         
        soundEffectsContinuous.loop = true;
        if (!soundEffectsContinuous.isPlaying) soundEffectsContinuous.Play();
    }

    public void StopContinuousEffect(AudioClip clip, bool immediately = false)
    {
        if (soundEffectsContinuous.clip == clip) 
        {
            soundEffectsContinuous.loop = false;
            if (immediately) soundEffectsContinuous.Stop();
        }
    }

}
