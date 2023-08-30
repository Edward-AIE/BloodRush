using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    GameManager gameManager;
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

    void Start()
    {
        ImageOff();
    }

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
        gameManager.EnemyKilled();
        Destroy(gameObject);
    }
}
