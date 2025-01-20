using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BodyFreeze : MonoBehaviour
{

    Rigidbody body;

    RigidbodyConstraints constraints = RigidbodyConstraints.None;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        constraints = body.constraints;
    }

    private void OnEnable() => Pause.OnPause += SetPause;
    private void OnDisable() => Pause.OnPause -= SetPause;


    private void SetPause(bool isPaused)
    {
        if (isPaused)
        {
            constraints = body.constraints;
            body.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            body.constraints = constraints;
        }
    }


}
