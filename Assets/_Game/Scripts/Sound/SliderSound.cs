using UnityEngine;

public class SliderSound : MonoBehaviour
{

    [SerializeField] MenuSoundData sounds;

    public void PlaySound() => SoundManager.instance.PlayEffect(sounds.Slider);
}
