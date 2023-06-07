using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float jumpForce;
    public float doubleJumpForce;

    [Header("Sprinting")]
    [SerializeField] float walkSpeed;
    [SerializeField] float sprintSpeed;
    [SerializeField] float acceleration = 10f;

    [Header("Sliding")]
    [SerializeField] Transform cameraPosition;
    [SerializeField] Transform nonSlideCamPosition;
    [SerializeField] Transform slideCamPosition;
    [SerializeField] float camPosChangeTime;
    [SerializeField] float slideDrag;
    [SerializeField] float slideCamTilt;
    [SerializeField] float slideJumpForce;

    [Header("Drag")]
    public float groundDrag = 6f;
    public float airDrag = 2;
    [SerializeField] float airMultiplier = -1f;

    [Header("Dash")]
    [SerializeField] float dashForce;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] KeyCode dashKey = KeyCode.LeftShift;
    [SerializeField] KeyCode slideKey = KeyCode.LeftControl;

    [Header("Other")]
    [SerializeField] Transform orientation;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Wallrun wallrun;

    float horizontalMovement;
    float verticalMovement;
    float playerHeight = 2f;
    
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public bool canDash;
    [HideInInspector] public bool isSliding;
    [HideInInspector] public bool canJump;
    [HideInInspector] public bool hasJumped;

    Vector3 moveDirection;
    Vector3 slopeMoveDirection;

    Rigidbody rb;

    RaycastHit slopeHit;

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.1f))
        {
            if (slopeHit.normal != Vector3.up)
            {
               return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position - new Vector3(0, 1, 0), groundDistance, groundLayer);
        
        if (canDash && !isGrounded)
        {
            if (Input.GetKeyDown(dashKey))
            {
                Dash();
                canDash = false;
            }
        }

        if (isGrounded)
        {
            hasJumped = false;
            canDash = true;
        }

        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(jumpKey) && !isGrounded && !hasJumped)
        {
            DoubleJump();
            hasJumped = true;
        }

        if (Input.GetKeyDown(jumpKey) && isSliding)
        {
            SlideJump();
        }

        if (isGrounded)
        {
            if (Input.GetKey(slideKey))
            {
                BeginSlide();
            }
            else if (!Input.GetKey(slideKey))
            {
                EndSlide();
            }
        }
        else
        {
            EndSlide();
        }

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);

        MyInput();
        ControlDrag();
        ControlSpeed();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
    }

    void MovePlayer()
    {
        if (!isSliding)
        {
            if (isGrounded && !OnSlope())
            {
                rb.AddForce(moveDirection * moveSpeed, ForceMode.Acceleration);

            }
            else if(OnSlope() && isGrounded)
            {
                rb.AddForce(slopeMoveDirection * moveSpeed, ForceMode.Acceleration);
            }
            else if (!isGrounded)
            {
                rb.AddForce(moveDirection * moveSpeed * airMultiplier, ForceMode.Acceleration);
            }
        }
    }

    void ControlDrag()
    {
        if (isGrounded)
        {
            if (isSliding)
            {
                rb.drag = slideDrag;
            }
            else
            {
                rb.drag = groundDrag;
            }
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    void ControlSpeed()
    {
        if (Input.GetKey(sprintKey) && isGrounded)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
        }
    }

    void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    void DoubleJump()
    {
        if (canJump)
        {
            rb.AddForce(transform.up * doubleJumpForce, ForceMode.Impulse);
        }
    }
    void SlideJump()
    {
        rb.AddForce(transform.up + orientation.forward * slideJumpForce, ForceMode.Impulse);
    }

    void Dash()
    {
        rb.velocity = new Vector3(0, rb.velocity.y, 0);
        
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-orientation.right * dashForce, ForceMode.Impulse);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(orientation.forward * dashForce, ForceMode.Impulse);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-orientation.forward * dashForce, ForceMode.Impulse);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(orientation.right * dashForce, ForceMode.Impulse);
        }
    }

    void BeginSlide()
    {
        isSliding = true;

        cameraPosition.position = Vector3.Lerp(cameraPosition.position, slideCamPosition.position, camPosChangeTime * Time.deltaTime);
        if(rb.velocity != new Vector3(0, 0, 0))
        {
            wallrun.tilt = Mathf.Lerp(wallrun.tilt, slideCamTilt, 20 * Time.deltaTime);
        }
    }

    void EndSlide()
    {
        isSliding = false;

        cameraPosition.position = Vector3.Lerp(cameraPosition.position, nonSlideCamPosition.position, camPosChangeTime * Time.deltaTime);
        if (rb.velocity != new Vector3(0, 0, 0))
        {
            wallrun.tilt = Mathf.Lerp(wallrun.tilt, 0, 20 * Time.deltaTime);
        }
    }
}
