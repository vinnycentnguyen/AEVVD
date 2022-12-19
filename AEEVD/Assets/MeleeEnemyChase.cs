using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyChase : MonoBehaviour
{
    public GameObject player;

    public float speed = 4f;

    private Rigidbody2D rb;

    private Vector2 movement;

    [SerializeField] private int damage = 1;

    public float cdTime;

    private float atkCD;

    private bool canAttack;

    
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        canAttack = true;
    }

    void Update()
    {   
       if(atkCD <= 0)
       {
        atkCD = cdTime;
        canAttack = true;
       }
       else
       {
        atkCD -= Time.deltaTime;
       }
       if(canAttack)
       {
            Vector3 direction = player.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            direction.Normalize();
            movement = direction;
       }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if(canAttack){
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            canAttack = false;
        }
    }
    
    private void FixedUpdate()
    {
        moveCharacter(movement);
    }

    void moveCharacter(Vector2 direction)
    {
        if(canAttack)
       {
            rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
       }
       else
       {
            rb.MovePosition((Vector2)transform.position - (direction * (speed * cdTime * 1.5f) * Time.deltaTime));
       }
    }

    
}
