using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HeadBob : MonoBehaviour
{
    [SerializeField] private bool enable = true;

    [SerializeField, Range(0, 0.1f)] private float amplitude = 0.015f;
    [SerializeField, Range(0, 30)] private float frequency = 10f;

    [SerializeField] private Transform cam = null;
    [SerializeField] private Transform camHolder = null;

    private float toggleSpeed = 3f;
    private Vector3 startPos;
    private CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        startPos = transform.position;
    }

    private Vector3 FootStepMotion()
    {
        //to do
    }
}
