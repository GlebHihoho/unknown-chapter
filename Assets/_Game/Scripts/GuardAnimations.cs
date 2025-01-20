using UnityEngine;
using UnityEngine.AI;

public class GuardAnimations : MonoBehaviour
{

    enum Status { Idle, Running, Helping }
    Status status = Status.Idle;

    Animator animator;

    NavMeshAgent agent;

    [SerializeField, Range(0, 100)] int changeThreshold = 80;
    [SerializeField, Min(1)] int idleAnimations = 4;

    [SerializeField] Transform foma;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() => InvokeRepeating(nameof(ChangeIdle), 0, 1);


    private void Update()
    {
        if (status == Status.Helping) return;

        if (status == Status.Idle && agent.remainingDistance > 0)
        {
            status = Status.Running;
        }

        if (status == Status.Running && agent.remainingDistance <= 0.1f)
        {
            status = Status.Helping;

            transform.LookAt(foma.position);
            animator.SetTrigger("Run");
            CancelInvoke(nameof(ChangeIdle));
        }
    }


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
