using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public GameObject Score;
    public HealthbarBehavior HealthBar;
    public ParticleSystem HitEffect;
    private SpriteRenderer m_SpriteRenderer;

    [SerializeField] public int scoreValue;
    public int health;

    private int maxHealth;

    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        maxHealth = health;
        HealthBar.SetHealth(health, maxHealth);
        Score = GameObject.FindGameObjectWithTag("Score");
    }
    
    public void TakeDamage(int damage)
    {
        for(int i = 0; i < damage; i++)
        {
            createHitEffect();
        }
        health -= damage;
        HealthBar.SetHealth(health, maxHealth);
        if(health <= 0)
        {
            Die();
        }
    }


    void createHitEffect()
    {
        var main = HitEffect.main;
        main.startColor = m_SpriteRenderer.color;
        Instantiate(HitEffect, transform.position, Quaternion.identity);
        HitEffect.Play();
    }

    void Die()
    {
        Score.gameObject.GetComponent<UpdateScore>().incrementScore(scoreValue);
        Destroy(gameObject);
    }
}
