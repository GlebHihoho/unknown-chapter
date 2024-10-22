using UnityEngine;

public class GuardAnimations : MonoBehaviour
{

    Animator animator;

    [SerializeField, Range(0, 100)] int changeThreshold = 80;
    [SerializeField, Min(1)] int idleAnimations = 4;

    private void Awake() => animator = GetComponent<Animator>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() => InvokeRepeating("ChangeIdle", 0, 1);
    

    private void ChangeIdle()
    {
        int random = Random.Range(0, 101);
        if (random >= changeThreshold) ChangeIdle(Random.Range(0, idleAnimations));
    }


    private void ChangeIdle(int index)
    {
        animator.SetInteger("IdleIndex", index);
        animator.SetTrigger("Change");
    }
}
