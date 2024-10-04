using System;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettingsUI : MonoBehaviour
{
    [SerializeField] Slider volume;
    [SerializeField] Slider music;
    [SerializeField] Slider effects;

    public static event Action<float> OnGeneralVolumeChange;
    public static event Action<float> OnMusicVolumeChange;
    public static event Action<float> OnEffectsVolumeChange;

    public static event Action OnCloseSettings;


    private void OnEnable()
    {
        volume.value = SoundManager.instance.MasterVolume;
        music.value = SoundManager.instance.MusicVolume;
        effects.value = SoundManager.instance.EffectsVolume;

        volume.onValueChanged.AddListener(ChangeGeneralVolume);
        music.onValueChanged.AddListener(ChangeMusicVolume);
        effects.onValueChanged.AddListener(ChangeEffectsVolume);
    }


    private void OnDisable()
    {
        volume.onValueChanged.RemoveListener(ChangeGeneralVolume);
        music.onValueChanged.RemoveListener (ChangeMusicVolume);
        effects.onValueChanged.RemoveListener(ChangeEffectsVolume);

        OnCloseSettings?.Invoke();
    }


    public void ChangeGeneralVolume(float volume) => OnGeneralVolumeChange?.Invoke(volume);
    public void ChangeMusicVolume(float volume) => OnMusicVolumeChange?.Invoke(volume);
    public void ChangeEffectsVolume(float volume) => OnEffectsVolumeChange?.Invoke(volume);
}
