using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject bullet;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ShootGun();
            StartCoroutine(ShootCooldown());
        }
    }

    void ShootGun()
    {

    }

    IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(0.3f);
    }
}
