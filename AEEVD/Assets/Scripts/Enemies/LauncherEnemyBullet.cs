using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherEnemyBullet : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    private Vector3 direction;
    public GameObject[] FirePoints;
    public GameObject LauncherEnemyBulletProjectiles;
    
    public float speed;
    public float cdTime;
    private float atkCD;
    private float slowFactor = 1;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = this.GetComponent<Rigidbody2D>();
        Invoke("Despawn", 5f);
        direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
        direction.Normalize();
        rb.velocity = direction * speed;
        atkCD = cdTime;
    }

    void FixedUpdate()
    {
        rb.velocity *= (slowFactor - Time.deltaTime);
        rb.rotation += 2f;
        if(atkCD <= 0)
        {
            Shoot();
            atkCD = cdTime;
        }
        else
        {
            atkCD -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        for(int i = 0; i < FirePoints.Length; i++)
        {
            Instantiate(LauncherEnemyBulletProjectiles, FirePoints[i].transform.position, FirePoints[i].transform.rotation);
        }
    }

    void Despawn()
    {
        Destroy(gameObject);
    }

    
}
