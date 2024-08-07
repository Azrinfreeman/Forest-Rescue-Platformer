using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //private Vector3 offset = new Vector3(0f, 0f, -10f);
    public Vector3 offset;
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField]
    private Transform target;

    private void Update()
    {
        if (PlayerController.instance.right)
        {
            offset = new Vector3(7, 3, -10);
            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(
                transform.position,
                targetPosition,
                ref velocity,
                smoothTime
            );
        }
        else
        {
            offset = new Vector3(0, 3, -10);
            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(
                transform.position,
                targetPosition,
                ref velocity,
                smoothTime
            );
        }
    }
}
