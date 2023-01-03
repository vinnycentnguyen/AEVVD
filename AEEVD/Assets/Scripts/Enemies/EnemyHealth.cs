using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public GameObject Score;
    public HealthbarBehavior HealthBar;

    [SerializeField] public int scoreValue;
    public int health;

    private int maxHealth;

    void Start()
    {
        maxHealth = health;
        HealthBar.SetHealth(health, maxHealth);
        Score = GameObject.FindGameObjectWithTag("Score");
    }
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        HealthBar.SetHealth(health, maxHealth);
        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Score.gameObject.GetComponent<UpdateScore>().incrementScore(scoreValue);
        Destroy(gameObject);
    }
}
