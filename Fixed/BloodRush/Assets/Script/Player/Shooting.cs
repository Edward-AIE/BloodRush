using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    Transform cam;
    public float shotRange;
    public float shotForce;
    bool shootable;
    public KeyCode shoot = KeyCode.Mouse0;
    Animator animator;
    RaycastHit hit;
    Transform muzzleLocation;
    public GameObject muzzleFlash;

    private void Start()
    {
        cam = Camera.main.transform;
        shootable = true;
        animator = GetComponentInChildren<Animator>();
        muzzleLocation = GameObject.Find("Muzzle Location").GetComponent<Transform>();
    }

    private void Update()
    {      
        if (Input.GetKeyDown(shoot) && shootable)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (canShoot())
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            Rigidbody enemyRb = hit.collider.GetComponent<Rigidbody>();
            enemy.TakeDamage(10);
            enemyRb.AddForce(cam.forward * shotForce, ForceMode.Impulse);
        }
        Instantiate(muzzleFlash, muzzleLocation.transform.position, muzzleLocation.transform.rotation, muzzleLocation);
        animator.SetTrigger("Shot Fired");
        StartCoroutine(BetweenShots());
    }

    private bool canShoot()
    {
        if (Physics.Raycast(cam.position, cam.forward, out hit, shotRange))
        {
            if (hit.collider.tag == "Enemy")
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

    IEnumerator BetweenShots()
    {
        shootable = false;
        yield return new WaitForSeconds(0.5f);
        shootable = true;
    }
}
