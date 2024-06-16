using UnityEngine;

public class SoundZoneBorder : MonoBehaviour
{

    [SerializeField] AudioClip clip1;
    [SerializeField] AudioClip clip2;


    [SerializeField] AudioSource source;

    [SerializeField, Range(1, 2)] int activeZone = 1; //TODO: Set zones angles?

    float enterDot, exitDot;

    [SerializeField, Range(0.01f, 1.5f)] float threshold = 0.5f;

    MeshRenderer render;



    private void Awake()
    {
        render = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            render.enabled = !render.enabled;
    }

    private void OnTriggerEnter(Collider other)
    {
        enterDot = DirectionDot(other);
    }

    private void OnTriggerExit(Collider other)
    {

        void ChangeClip(AudioClip clip)
        {
            source.Stop();
            if (clip != null)
            {
                source.clip = clip;
                source.Play();
            }
                
        }

        exitDot = DirectionDot(other);

        float direction = Mathf.Abs(enterDot - exitDot); //TODO: Small angles at big distance from the center of trigger

        Debug.Log("Direction: " + direction);

        if (direction >= threshold)
        {
            if (activeZone == 1) 
            { 
                activeZone = 2;
                ChangeClip(clip2);
            }
            else 
            { 
                activeZone = 1;
                ChangeClip(clip1);
            }

            Debug.Log("Changed zone to " + activeZone);

        }
                
    }


    private float DirectionDot(Collider target)
    {
        Vector3 directionToTarget = Vector3.Normalize(target.transform.position - transform.position);

        return Vector3.Dot(transform.forward, directionToTarget);
    }
}
