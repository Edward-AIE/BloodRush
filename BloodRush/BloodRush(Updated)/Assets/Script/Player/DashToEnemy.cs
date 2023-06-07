using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashToEnemy : MonoBehaviour
{
    // Keybind
    [Header("Keybind")]
    [SerializeField] private KeyCode dashKey = KeyCode.Mouse1;

    // Values for dashing functionality based on whether or not the player is grounded
    [Header("Multipliers")]
    [SerializeField] private float airDashForceMulti = 2f;
    [SerializeField] private float groundDashForceMulti = 7f;

    // Variables for the raycast
    [Header("Enemy Layer")]
    [SerializeField] private LayerMask enemy;
    private RaycastHit hitInfo;

    // Variables to hold the data grabbed out of the enemy
    private Transform enemyDashSpot;
    private Enemy temp;  
    
    // Stuff about the player
    private Rigidbody rb;
    private Camera cam;
    private PlayerMovement player;

    // Variables specifically for this script
    private bool canDash;
    private float dashForce;
    private Vector3 direction;
    private float distance;

    // Cooldown
    [Header("Cooldown")]
    [SerializeField] private float seconds;
    private float lastDash;
    [HideInInspector] public float timeSinceDash;

    private void Start()
    {
        canDash = false;
        
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        player = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {      
        if (enemyDashSpot != null)
        {
            direction = (enemyDashSpot.position - transform.position).normalized;
            distance = Vector3.Distance(enemyDashSpot.position, transform.position);

            if (player.isGrounded)
            {
                dashForce = distance * groundDashForceMulti;
            }
            else if (!player.isGrounded)
            {
                dashForce = distance * airDashForceMulti;
            }
        }
        
        if (canDash)
        {
            if (Input.GetKeyDown(dashKey))
            {
                lastDash = Time.time;
                rb.velocity = Vector3.zero;
                rb.AddForce(direction * dashForce, ForceMode.Impulse);
                temp.ImageOff();
            }
        }
        
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, 20f, enemy))
        {
            if (hitInfo.collider.tag == "Enemy")
            {
                temp = hitInfo.collider.GetComponent<Enemy>();
                enemyDashSpot = temp.GetDashPosition();
                
                if (timeSinceDash == seconds)
                {
                    canDash = true;
                    temp.ImageOn();
                }
            }
            else
            {
                NoDash();
            }
        }
        else
        {
            NoDash();
        }

        timeSinceDash = Time.time - lastDash;

        if (timeSinceDash >= seconds)
        {
            timeSinceDash = seconds;
        }
    }

    private void NoDash()
    {
        if (temp != null)
        {
            temp.ImageOff();
            canDash = false;
        }
    }
}
