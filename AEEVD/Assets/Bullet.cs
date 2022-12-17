using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8f;
    public Rigidbody2D rb;

    void Start()
    {
        rb.velocity = transform.up * speed;
        Invoke("despawn", 1f);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
<<<<<<< HEAD:AEEVD/Assets/Bullet.cs
        EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage);
            Debug.Log(enemy);
        }
        Debug.Log("created");
=======
        Debug.Log(hitInfo.name);
>>>>>>> parent of 7358655 (Player/enemy take damage and Enemy movement):AEEVD/Assets/Scripts/Bullet.cs
        Destroy(gameObject);
    }

    void despawn()
    {
        Destroy(gameObject);
    }
}
