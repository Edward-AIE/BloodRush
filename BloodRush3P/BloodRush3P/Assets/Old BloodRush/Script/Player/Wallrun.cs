using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallrun : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    
    [SerializeField] Transform orientation;

    [Header("Wall Running")]
    [SerializeField] float wallDistance = 0.5f;
    [SerializeField] float minimumJumpHeight = 1.5f;
    [SerializeField] float wallRunGravity;
    [SerializeField] float wallRunJumpForce;

    [Header("Camera")]
    [SerializeField] Camera cam;
    [SerializeField] float fov;
    [SerializeField] float wallFov;
    [SerializeField] float wallFovTime;
    [SerializeField] float camTilt;
    [SerializeField] float camTiltTime;

    public float tilt;

    bool wallLeft = false;
    bool wallRight = false;

    RaycastHit leftWallHit;
    RaycastHit rightWallHit;

    private Rigidbody rb;
    
    bool CanWallRun()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minimumJumpHeight);
    }

    void CheckWall()
    {
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallDistance);
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallDistance);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    private void Update()
    {
        CheckWall();

        if (CanWallRun())
        {
            if (wallLeft)
            {
                StartWallRun();
            }
            else if (wallRight)
            {
                StartWallRun();
            }
            else
            {
                StopWallRun();
            }
        }
        else
        {
            StopWallRun();
        }
    }

    void StartWallRun()
    {
        rb.useGravity = false;
        playerMovement.canJump = false;
        playerMovement.hasJumped = false;
        rb.AddForce(Vector3.down * wallRunGravity, ForceMode.Force);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, wallFov, wallFovTime * Time.deltaTime);

        if (wallLeft)
        {
            tilt = Mathf.Lerp(tilt, -camTilt, camTiltTime * Time.deltaTime);
        }
        else if(wallRight)
        {
            tilt = Mathf.Lerp(tilt, camTilt, camTiltTime * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (wallLeft)
            {
                Vector3 wallRunJumpDirection = transform.up + leftWallHit.normal;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(wallRunJumpDirection * wallRunJumpForce * 100, ForceMode.Force);
            }
            else if (wallRight)
            {
                Vector3 wallRunJumpDirection = transform.up + rightWallHit.normal;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(wallRunJumpDirection * wallRunJumpForce * 100, ForceMode.Force);
            }
        }
    }

    void StopWallRun()
    {
        rb.useGravity = true;
        playerMovement.canJump = true;
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, wallFovTime * Time.deltaTime);
        tilt = Mathf.Lerp(tilt, 0, camTiltTime * Time.deltaTime);
    }
}
