using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] Camera cam;
    [SerializeField] private Vector3 offset;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        weapon.transform.position = cam.transform.position + offset;
        weapon.transform.localRotation = cam.transform.rotation;
    }
}
