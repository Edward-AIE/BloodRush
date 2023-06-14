using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float _maxHealth;
    public float health;
    [SerializeField] private FloatingHealthbar healthbar;
    [SerializeField] private Image dashConfirm;
    [SerializeField] private Transform _DashPosition;

    public Transform GetDashPosition()
    {
        return _DashPosition;
    }

    public Image GetImage()
    {
        return dashConfirm;
    }

    public void ImageOn()
    {
        dashConfirm.enabled = true;
    }

    public void ImageOff()
    {
        dashConfirm.enabled = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        ImageOff();
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.UpdateHealthBar(health, _maxHealth);
    }

    public void TakeDamage(float damageAmout)
    {

        health -= damageAmout;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {

    }
}
