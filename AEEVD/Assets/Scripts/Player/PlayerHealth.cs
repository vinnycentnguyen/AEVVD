using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image healthBar;
    public Gradient gradient;

    private int health = 10;
    private float maxHealth;
    private bool showHP;
    private bool startFade;
    private float newAlpha;
    private float timer;
    private float fadeTimer;
    

    void Start()
    {  
        newAlpha = 1;
        timer = 3f;
        fadeTimer = 0.35f;
        showHP = true;
        startFade = false;
        alive = true;
        maxHealth = 10f;
    }

    void FixedUpdate()
    {
        if(showHP && health > 3)
        {
            newAlpha = 1;
            ChangeAlpha();
            timer -= Time.deltaTime;
            if(timer <= 0)
            {  
                showHP = false;
                startFade = true;
            }
        }
        else if(startFade)
        {
            fadeTimer -= Time.deltaTime;
            if(fadeTimer <= 0)
            {
                newAlpha -= 0.025f;
                ChangeAlpha();
                fadeTimer = 0.05f;
            }
            if(newAlpha <= 0)
            {
                startFade = false;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.fillAmount = health/maxHealth;
        healthBar.color = gradient.Evaluate(healthBar.fillAmount);
        showHP = true;
        startFade = false;
        timer = 3f;
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
        showHP = true;
    }

    public void ChangeAlpha()
    {
        Color newColor = healthBar.color;
        newColor.a = newAlpha;
        healthBar.color = newColor;
    }
    
    void Die()
    {
        Destroy(gameObject);
    }

    public bool alive{get; private set;}
}
