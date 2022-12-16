using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyChase : MonoBehaviour
{
    public Transform player;

    public float speed = 4f;

    private Rigidbody2D rb;

    private Vector2 movement;

    [SerializeField] private int damage = 1;

    
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {   
        Debug.Log(player.gameObject.GetComponent<PlayerHealth>().alive);
        if (player.gameObject.GetComponent<PlayerHealth>().alive){
            Vector3 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            direction.Normalize();
            movement = direction;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
    private void FixedUpdate()
    {
        if (player.gameObject.GetComponent<PlayerHealth>().alive){
            moveCharacter(movement);
        }
    }

    void moveCharacter(Vector2 direction)
    {
        if (player.gameObject.GetComponent<PlayerHealth>().alive)
        {
            rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
        }
    }

    
}
