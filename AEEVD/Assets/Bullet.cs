using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8f;

    public Rigidbody2D rb;
    
    public int damage = 1;

    void Start()
    {
        rb.velocity = transform.up * speed;
        Invoke("despawn", 1);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
    }

    void despawn()
    {
        Destroy(gameObject);
    }
}
