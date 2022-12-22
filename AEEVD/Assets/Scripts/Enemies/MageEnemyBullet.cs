using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageEnemyBullet : MonoBehaviour
{
    
    public GameObject yellow;
    public GameObject orange;
    public GameObject red;
    
    public float splashRange = 1;

    private float timer;
    private int rotation;
    private int damage = 2;
    private bool inflict;
    private float inflictTimer;

    void Start()
    {   
        inflict = true;
        rotation = 0;
        timer = 0;
        inflictTimer = 0;
        Invoke("DestroyProjectile", 1.25f);
    }

    void Update()
    {
        if(timer < 0.25f)
        {
            timer += Time.deltaTime;
        }
        else if(timer >= 0.25 && rotation < 3)
        {
            createEffect(rotation);
            rotation++;
            timer = 0;
            Debug.Log(inflictTimer);
        }
    }

    void FixedUpdate()
    {
        inflictTimer += Time.deltaTime;
    }

    private void createEffect(int rotation)
    {
        switch(rotation)
        {
            case 0:
                Instantiate(yellow, transform.position, Quaternion.identity);
                break;
            case 1:
                Instantiate(orange, transform.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(red, transform.position, Quaternion.identity);
                break;
            default:
                Debug.Log("Out of bounds: rotation");
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Physics2D.OverlapCircle(transform.position, splashRange, 3) && inflict && (inflictTimer > 0.8))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            inflict = false;
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
