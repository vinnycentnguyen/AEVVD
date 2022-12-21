using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageEnemyChase : MonoBehaviour
{
    public GameObject EnemySpell;
    public GameObject player;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector3 direction;


    public float speed;
    private bool inRange;
    public float attackRange;
    public float cdTime;
    private float atkCD;
    public float radius = 1.5f;

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
                Instantiate(EnemySpell, (Vector2)player.transform.position + Random.insideUnitCircle * radius, Quaternion.identity);
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
