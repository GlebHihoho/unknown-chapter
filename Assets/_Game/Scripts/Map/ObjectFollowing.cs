using UnityEngine;

public class ObjectFollowing : MonoBehaviour
{
    [SerializeField] Transform target;

    [SerializeField] bool rotate = false;

    [SerializeField] float smoothTime = 0.2f;
    [SerializeField] float smoothRotateTime = 0.1f;

    Vector3 velocity = Vector3.zero;
    Vector3 rotateVelocity = Vector3.zero;


    // Update is called once per frame
    void Update()
    {

        Vector3 newPos = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, smoothTime);

        if (rotate)
        {
            Vector3 newRotation = new Vector3(90, target.rotation.eulerAngles.y, 0);
            transform.rotation = Quaternion.Euler(Vector3.SmoothDamp(transform.rotation.eulerAngles, newRotation, ref rotateVelocity, smoothRotateTime));
        }
    }
}
