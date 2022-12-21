using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageEnemyBullet : MonoBehaviour
{
    private int damage = 2;
    public GameObject yellow;
    public GameObject orange;
    public GameObject red;

    private float timer;
    private bool cast;

    void Start()
    {   
        cast = true;
        timer = 0;
        Invoke("DestroyProjectile", 1f);
    }

    void Update()
    {
        if(cast){
            if(timer >=0 && timer < 0.25)
            {
                Instantiate(yellow, transform.position, Quaternion.identity);
                cast = false;
            }
            else if(timer >= 0.25 && timer < 0.5)
            {
                Instantiate(orange, transform.position, Quaternion.identity);
                cast = false;
            }
            
            else if(timer >= 0.5 && timer < 0.75)
            {
                Instantiate(red, transform.position, Quaternion.identity);
                cast = false;
            }
        }

        timer += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(timer >= 0.85 && timer < 100)
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            timer = 100;
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
