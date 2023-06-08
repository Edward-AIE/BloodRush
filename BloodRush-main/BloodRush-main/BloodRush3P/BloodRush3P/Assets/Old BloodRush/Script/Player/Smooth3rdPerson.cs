using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smooth3rdPerson : MonoBehaviour
{
    public Transform target;
    Vector3 offset;
    Vector3 angleOffset;

    public float CameraMoveSmoothing;
    public float CameraRotateSmoothing;
    Vector3 cameraMoveSpeed;
    Vector3 cameraRotateSpeed;
    void ResetOffsets()
    {
        if (target == null)
            return;
        offset = target.position - transform.position;
        Quaternion directionDifference = Quaternion.FromToRotation(target.forward, transform.forward);
        angleOffset = directionDifference.eulerAngles;
    }
    void Awake ()
    {
        ResetOffsets();
    }

    void LateUpdate ()
    {
        if (target == null)
            return;
        Vector3 desiredPoition = target.position - target.rotation * offset;
        Quaternion desiredRotation = target.rotation * Quaternion.Euler(angleOffset);

        transform.position = Vector3.SmoothDamp(transform.position, desiredPoition, ref cameraMoveSpeed, CameraMoveSmoothing);
    }
}


