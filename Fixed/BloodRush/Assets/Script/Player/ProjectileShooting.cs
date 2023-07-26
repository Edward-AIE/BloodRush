using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooting : MonoBehaviour
{
    public Transform gunAimPoint;
    public Transform gun;

    public Transform gunPoint;
    public float distance = 200;
    public float hitForce = 100;

    void Update()
    {
        gun.LookAt(gunAimPoint.forward * 100);

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

    }

    void Shoot()
    {
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(gunPoint.position, gunPoint.forward, out hit, distance))
        {
            Debug.Log(hit.transform.name);
            Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
            if (rb != null)
            {

                rb.AddForce(gun.forward * hitForce, ForceMode.Impulse);
            }
        }
    }

}
