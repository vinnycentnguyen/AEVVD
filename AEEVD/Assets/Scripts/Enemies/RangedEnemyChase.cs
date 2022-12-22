using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyChase : MonoBehaviour
{
    
    public GameObject EnemyBullet;
    public GameObject player;
    public Transform rangedFirePoint;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector3 direction;


    public float speed;
    private bool inRange;
    public float attackRange;
    public float cdTime;
    private float atkCD;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = this.GetComponent<Rigidbody2D>();
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
            inRange = true;
        }
        else
        {
            inRange = false;
        }

        if(Vector2.Distance(transform.position, player.transform.position) <= attackRange)
        {
            if(atkCD <= 0)
            {
                Instantiate(EnemyBullet, rangedFirePoint.position, rangedFirePoint.transform.rotation);
                atkCD = cdTime;
            }
            else
            {
                atkCD -= Time.deltaTime;
            }
        }
    }

    void FixedUpdate()
    {
        if(inRange)
        {
            rb.MovePosition((Vector2)transform.position + (Vector2)(direction * speed * Time.deltaTime));       
        }
    }
}
