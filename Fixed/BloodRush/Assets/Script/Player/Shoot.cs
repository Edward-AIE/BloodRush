using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private float shotForce;
    [SerializeField] private Rigidbody bullet;
    private Transform instLocation;
    private Vector3 shotDirection;
    private Transform cam;
    private RaycastHit hit;

    private void Start()
    {
        cam = Camera.main.GetComponent<Transform>();
        instLocation = GameObject.Find("Instantiate Location").GetComponent<Transform>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ShootGun();
            StartCoroutine(ShootCooldown());
        }

        Physics.Raycast(cam.position, cam.transform.forward, out hit, Mathf.Infinity);

        Vector3 hitSpot = hit.transform.position;

        shotDirection = new Vector3(cam.position.x - hitSpot.x, cam.position.y - hitSpot.y, cam.position.z - hitSpot.z).normalized;
    }

    void ShootGun()
    {
        Rigidbody bulletRb = Instantiate(bullet, instLocation);
        bulletRb.AddForce(shotDirection * shotForce, ForceMode.Impulse);
    }

    IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(0.3f);
    }
}
