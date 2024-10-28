using UnityEngine;


public class PlayerFootsteps : MonoBehaviour
{

    AudioSource source;
  

    [SerializeField] FootstepsData footstepsData;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        source.clip = footstepsData.DefaultFootsteps;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MouseInput.instance.OnDestinationSet += StartSteps;
        MouseInput.instance.OnDeleteMovePoint += StopSteps;
    }

    private void OnDestroy()
    {
        MouseInput.instance.OnDestinationSet -= StartSteps;
        MouseInput.instance.OnDeleteMovePoint -= StopSteps;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartSteps()
    {
        if (!source.isPlaying) source.Play();
    }

    public void StopSteps()
    {
        source.Stop();
    }
}
