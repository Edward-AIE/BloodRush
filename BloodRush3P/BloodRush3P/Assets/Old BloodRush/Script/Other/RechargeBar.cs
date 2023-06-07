using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RechargeBar : MonoBehaviour
{
    private DashToEnemy dashToEnemy;
    private Slider bar;
    
    // Start is called before the first frame update
    void Start()
    {
        dashToEnemy = GameObject.Find("Player").GetComponentInChildren<DashToEnemy>();
        bar = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        bar.value = dashToEnemy.timeSinceDash;
    }
}
