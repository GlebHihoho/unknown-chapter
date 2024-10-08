using UnityEngine;


public class AnimationsManager : MonoBehaviour
{
    Animator animator;

    [SerializeField, Range(0, 100)] int changeThreshold = 85;
    [SerializeField, Range(0, 100)] int sadThreshold = 97;

    private void Awake() => animator = GetComponent<Animator>();

    private void Start() => InvokeRepeating(nameof(ChangeAnimation), 0, 1);

    private void ChangeAnimation()
    {
        int random = Random.Range(0, 101);

        if (random >= sadThreshold) animator.SetTrigger("PlaySad");
        else if (random >= changeThreshold) animator.SetTrigger("ChangeIdle");
    }
}
