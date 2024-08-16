using DG.Tweening;
using UnityEngine;

public class ObjectFollowing : MonoBehaviour
{
    [SerializeField] Transform target;

    [SerializeField] bool rotate = false;


    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.DOMove(newPos, 0.1f);

        if (rotate)
        {
            Vector3 newRotation = new Vector3(90, target.rotation.eulerAngles.y, 0);
            transform.DORotate(newRotation, 0.5f);
        }
    }
}
