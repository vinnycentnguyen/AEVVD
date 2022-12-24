using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public int scoreValue;
    public GameObject Score;
    public int health;
    
    void Start()
    {
        Score = GameObject.FindGameObjectWithTag("Score");
    }
    
    public void TakeDamage(int damage)
    {
        health -= damage;

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
