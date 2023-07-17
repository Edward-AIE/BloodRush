using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{
    [SerializeField] private bool enable = true;

    [SerializeField, Range(0, 0.1f)] private float amplitude = 0.002f;
    [SerializeField, Range(0, 30)] private float frequency = 10f;

    private Transform cam = null;
    [SerializeField] private Transform camHolder = null;

    private float toggleSpeed = 3f;
    private Vector3 startPos;
    private PlayerMovement controller;
    private Rigidbody rb;

    private void Awake()
    {
        controller = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
        cam = Camera.main.GetComponent<Transform>();
        startPos = cam.localPosition;
    }

    private void Update()
    {
        if (!enable)
        {
            return;
        }

        CheckMotion();
        ResetPosition();
        cam.LookAt(FocusTarget());
    }

    private void PlayMotion(Vector3 motion)
    {
        cam.localPosition += motion;
    }

    private void CheckMotion()
    {
        float speed = new Vector3(rb.velocity.x, 0, rb.velocity.z).magnitude;

        if (speed < toggleSpeed) return;
        if (!controller.isGrounded) return;

        PlayMotion(FootStepMotion());
    }

    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * frequency) * amplitude;
        pos.x += Mathf.Cos(Time.time * frequency) * amplitude;
        return pos;
    }

    private void ResetPosition()
    {
        if (cam.localPosition == startPos) return;
        cam.localPosition = Vector3.Lerp(cam.localPosition, startPos, 1 * Time.deltaTime);
    }

    private Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + camHolder.localPosition.y, transform.position.z);
        pos += camHolder.forward * 15f;
        return pos;
    }
}
