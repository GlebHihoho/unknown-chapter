using UnityEngine;

public class NPCAnimationManager : MonoBehaviour
{

    Animator animator;

    [SerializeField, Min(0)] int talkAnimations;

    [SerializeField, Min(0)] float minChangeTime = 1;
    [SerializeField, Min(0)] float maxChangeTime = 5;

    float changeTimer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        changeTimer = Random.Range(minChangeTime, maxChangeTime);
    }


    // Update is called once per frame
    void Update()
    {
        changeTimer -= Time.deltaTime;

        if (changeTimer < 0)
        {
            changeTimer = Random.Range(minChangeTime, maxChangeTime);

            animator.SetInteger("Talk", Random.Range(0, talkAnimations));
            animator.SetTrigger("Change");
        }
    }
}
