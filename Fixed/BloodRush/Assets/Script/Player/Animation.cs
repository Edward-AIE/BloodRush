using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    private PlayerMovement controller;
    public Animator animator;
    
    private float speed;
    private float motionspeed;

    [SerializeField] private float jumpingThreshold;

    private void Start()
    {
        controller = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (controller.sprinting && controller.isGrounded)
        {
            speed = 6;
        }
        else if(!controller.sprinting && controller.isGrounded)
        {
            speed = 2;
        }
        else
        {
            speed = 0;
        }

        if (controller.rb.velocity.y > jumpingThreshold)
        {
            animator.SetBool("Jump", true);
        }
        if (controller.rb.velocity.y < -jumpingThreshold)
        {
            animator.SetBool("FreeFall", true);
        }
        if (controller.isGrounded)
        {
            animator.SetBool("Grounded", true);
            animator.SetBool("Jump", false);
            animator.SetBool("FreeFall", false);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) && controller.isGrounded)
        {
            animator.SetFloat("Speed", speed);
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W) && controller.isGrounded)
        {
            animator.SetFloat("Speed", 0);
        }
    }
}
