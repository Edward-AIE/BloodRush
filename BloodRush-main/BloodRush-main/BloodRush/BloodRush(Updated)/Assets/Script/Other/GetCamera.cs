using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCamera : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    
    // Start is called before the first frame update
    void Start()
    {
        canvas.worldCamera = Camera.main;
    }
}
