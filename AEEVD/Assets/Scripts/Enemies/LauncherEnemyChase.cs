using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherEnemyChase : MonoBehaviour
{
    public GameObject LauncherEnemyBullet;
    public GameObject player;
    public Transform launcherFirePoint;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector3 direction;

    public float attackRange;
    public float cdTime;
    public float speed;

    private bool inRange;
    private bool hit;
    private float hitTimer;
    private float atkCD;
    private float activeSpeed;
    private int collisionDamage = 5;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = this.GetComponent<Rigidbody2D>();
        hitTimer = 0.5f;
        activeSpeed = speed;
    }


    void Update()
    {
        direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;

        if(Vector2.Distance(transform.position, player.transform.position) > attackRange)
        {
            activeSpeed = speed;
        }
        else
        {
            activeSpeed = 2.3f;
        }

        if(Vector2.Distance(transform.position, player.transform.position) <= attackRange)
        {
            if(atkCD <= 0)
            {
                Instantiate(LauncherEnemyBullet, launcherFirePoint.position, launcherFirePoint.transform.rotation);
                atkCD = cdTime;
            }
            else
            {
                atkCD -= Time.deltaTime;
            }
        }

        if(hit)
        {
            hitTimer -= Time.deltaTime;
            if(hitTimer <= 0)
            {
                hit = false;
            }
        }
    }

    void FixedUpdate()
    {
        if(hit == false)
        {
            rb.MovePosition((Vector2)transform.position + (Vector2)(direction * activeSpeed * Time.deltaTime));
        }
        else
        {
            rb.MovePosition((Vector2)transform.position - (Vector2)(direction * (activeSpeed * 1.5f) * Time.deltaTime));
        }  
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(hit == false){
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(collisionDamage);
            hit = true;
            hitTimer = 0.5f;
        }
        rb.velocity = Vector2.zero;
    }
}
