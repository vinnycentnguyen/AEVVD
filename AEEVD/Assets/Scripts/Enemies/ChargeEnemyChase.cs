using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class ChargeEnemyChase : MonoBehaviour
{
    public Rigidbody2D target;
    public GameObject player;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector3 direction;
    
    public float speed;
    public float attackRange;
    public float cdTime;
    public int damage;

    private bool inRange;
    private float timer;
    private float chargeTimer;
    private float atkCD;
    private bool canCharge;

    void Start()
    {
        canCharge = false;
        timer = 1f;
        chargeTimer = 0.5f;
        rb = this.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
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
                charge();
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
        else
        {
            timer -= Time.deltaTime;
        }
        if(timer <= 0)
        {
            canCharge = true;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if(canCharge){
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            canCharge = false;
        }
    }

    void charge()
    {
        chargeTimer = 0.5f;
        speed = 10f;
        if(InterceptionDirection(a: target.transform.position, b: transform.position, vA: target.velocity, speed, result: out var chargeDirection))
        {
            if(canCharge)
            {
                Debug.Log("Charging");
                rb.velocity = chargeDirection * speed;
                chargeTimer -= Time.deltaTime;
                if(chargeTimer <= 0)
                {
                    canCharge = false;
                    speed = 4f;
                }
            }
        }
    }

    public bool InterceptionDirection(Vector2 a, Vector2 b, Vector2 vA, float sB, out Vector2 result)
    {
        var aToB = b - a;
        var dC = aToB.magnitude;
        var alpha = Vector3.Angle(aToB, vA) * Mathf.Deg2Rad;
        var sA = vA.magnitude;
        var r = sA / sB;
        if(MyMath.SolveQuadratic(a: 1 - r * r, b: 2 * r * dC * Mathf.Cos(alpha), c: -(dC * dC), out var root1, out var root2) == 0)
        {
            result = Vector2.zero;
            return false;
        }
        var dA = Mathf.Max(a: root1, b: root2);
        var t = dA / sB;
        var c = a + vA * t;
        result = (c - b).normalized;
        Debug.Log(result);
        return true; 
    }
}

public class MyMath
{
    public static int SolveQuadratic(float a, float b, float c, out float root1, out float root2)
    {
        var discriminant = b * b - 4 * a * c;
        if(discriminant < 0)
        {
            root1 = Mathf.Infinity;
            root2 = -root1;
            return 0;
        }
            root1 = (-b + Mathf.Sqrt(discriminant))/(2 * a);
            root2 = (-b - Mathf.Sqrt(discriminant))/(2 * a);
            return discriminant > 0 ? 2 : 1;
    }
}
