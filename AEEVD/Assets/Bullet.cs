using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8;
    public Rigidbody2D rb;
    public int damage = 1;

    void Start()
    {
        rb.velocity = transform.up * speed;
        Invoke("despawn", 0.7f);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        EnemyHealth enemy = hitInfo.GetComponent<EnemyHealth>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
            Debug.Log(gameObject.tag);
        }
        Debug.Log("created");
    }

    void despawn()
    {
        Destroy(gameObject);
    }
}
