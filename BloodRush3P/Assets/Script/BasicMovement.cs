using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{

    Animator anim;

    [Header("Movement")]
    public float moveSpeed = 6f;
    public float jumpForce = 10f;
    public float groundDrag = 6f;
    public float airDrag = 2f;
    public float airMultiplier = 0.3f;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;

    float horizontalMovement;
    float verticalMovement;

    [Header("Other stuff")]
    [SerializeField] bool isGrounded;
    bool isRunning;
    bool startJumping;
    bool isDucking;

    Transform groundCheck;
    Transform camRig;
    [SerializeField] LayerMask groundLayer;

    Vector3 moveDirection;

    Rigidbody rb;

    float previousVelocity;

    private void Animation()
    {

        float forwardAnimation = verticalMovement;

        if(startJumping == true)
        {
            anim.SetTrigger("Jump");
            startJumping = false;
        }
        anim.SetBool("IsDucking", isDucking);


        anim.SetFloat("Forward", forwardAnimation);
        anim.SetBool("IsGrounded", isGrounded);
        anim.SetFloat("VerticalVelocity", rb.velocity.y);
        anim.SetFloat("PrevVerticalVelocity", previousVelocity);
        previousVelocity = rb.velocity.y;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        groundCheck = GameObject.Find("Ground Check").GetComponent<Transform>();
        camRig = GameObject.Find("CameraRig").GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    

    private void Update()
    {
        MyInput();
        ControlDrag();

        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);

        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            startJumping = true;
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        if(Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Attack");
        }
       
    }

    void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
        isDucking = Input.GetKey(KeyCode.LeftControl);

        moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;
    }
    
    private void FixedUpdate()
    {
        MovePlayer();
        transform.rotation = camRig.transform.rotation;

        Animation();
    }

    void MovePlayer()
    {
        if (isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Acceleration);
        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier, ForceMode.Acceleration);
        }
    }

    void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else if (!isGrounded)
        {
            rb.drag = airDrag;
        }
    }
    

}





