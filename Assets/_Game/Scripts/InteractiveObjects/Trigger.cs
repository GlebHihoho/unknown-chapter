using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{

    public UnityEvent OnTrigger;


    private void OnTriggerEnter(Collider other)
    {
        OnTrigger.Invoke();
    }
}
