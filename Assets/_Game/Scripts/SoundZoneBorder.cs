using UnityEngine;

public class SoundZoneBorder : MonoBehaviour
{

    [SerializeField] AudioClip frontalClip;
    [SerializeField] AudioClip backClip;

    [SerializeField] AudioSource source;


    float enterDot, exitDot;

    MeshRenderer render;


    private void Awake() => render = GetComponent<MeshRenderer>();


    private void Start() => GameConsole.instance.OnToggleTriggersView += ToggleTriggerVisibility;
    private void OnDestroy() => GameConsole.instance.OnToggleTriggersView -= ToggleTriggerVisibility;
    private void ToggleTriggerVisibility() => render.enabled = !render.enabled;


    private void OnTriggerEnter(Collider other) => enterDot = DirectionDot(other);

    private void OnTriggerExit(Collider other)
    {

        void ChangeClip(AudioClip clip)
        {

            if (clip != null) 
            {
                if (source.clip != clip || source.clip == null)
                {
                    source.Stop();
                    source.clip = clip;
                    source.Play();
                }
            }
            else if (source.clip != null)
            {
                source.Stop();
                source.clip = null;
            }
                
        }

        exitDot = DirectionDot(other);

        float direction = enterDot * exitDot;


        if (direction < 0)
        {
            if (exitDot > 0) 
            { 
                ChangeClip(frontalClip);
            }
            else 
            { 
                ChangeClip(backClip);
            }

        }
                
    }


    private float DirectionDot(Collider target)
    {
        Vector3 directionToTarget = Vector3.Normalize(target.transform.position - transform.position);

        return Vector3.Dot(transform.forward, directionToTarget);
    }
}
