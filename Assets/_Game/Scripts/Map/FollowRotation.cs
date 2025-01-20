using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FollowRotation : MonoBehaviour
{

    [SerializeField] Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newRotation = new Vector3(90, target.rotation.eulerAngles.y, 0);

        transform.DORotate(newRotation, 3);
    }
}
