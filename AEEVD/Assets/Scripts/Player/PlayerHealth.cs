using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private int health = 10;
    private float maxHealth;
    public Image healthBar;

    void Start()
    {
        alive = true;
        maxHealth = 10f;
    }
    void Update()
    {
        healthBar.fillAmount = health/maxHealth;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            healthBar.fillAmount = 0;
            health = 0;
            alive = false;
            Debug.Log("Player Dead");
            Die();
        }
    }

    public void healEveryRound(int currentWaveNum)
    {
        if(health + (currentWaveNum / 10) + 2 <= 10)
        {
            health += 2;
        }
        else
        {
            health = 10;
        }
    }
    
    void Die()
    {
        Destroy(gameObject);
    }

    public bool alive{get; private set;}
}
