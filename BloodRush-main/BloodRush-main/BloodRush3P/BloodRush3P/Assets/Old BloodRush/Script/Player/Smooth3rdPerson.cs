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
        Vector3 desiredPosition = target.position - target.rotation * offset;
        Quaternion desiredRotation = target.rotation * Quaternion.Euler(angleOffset);

        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref cameraMoveSpeed, CameraMoveSmoothing);
        transform.rotation = SmoothlyRotate(transform.rotation, desiredRotation, ref cameraRotateSpeed, CameraRotateSmoothing);
    }

    Quaternion SmoothlyRotate(Quaternion from, Quaternion to, ref Vector3 velocity, float smoothTime)
    {
        Vector3 desiredAngles = to.eulerAngles;
        Vector3 currentAngles = from.eulerAngles;
        Vector3 newAngles = new Vector3();

        newAngles.x = Mathf.SmoothDampAngle(currentAngles.x, desiredAngles.x, ref velocity.x, smoothTime);
        newAngles.y = Mathf.SmoothDampAngle(currentAngles.y, desiredAngles.y, ref velocity.y, smoothTime);
        newAngles.z = Mathf.SmoothDampAngle(currentAngles.z, desiredAngles.z, ref velocity.z, smoothTime);

        return Quaternion.Euler(newAngles);
    }
}


