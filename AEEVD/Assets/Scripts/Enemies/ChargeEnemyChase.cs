using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class ChargeEnemyChase : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    private Vector3 direction;
    
    public float speed;
    public float attackRange;
    public float cdTime;
    public int damage;

    private bool inRange;
    private float timer;
    private float atkCD;
    private float chargeAngle;
    private bool canCharge;
    private bool hit;
    private bool charging;

    void Start()
    {
        charging = false;
        hit = false;
        atkCD = cdTime;
        canCharge = false;
        timer = 0.35f;
        rb = this.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        if(charging == false)
        {
            direction = player.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
            direction.Normalize();
        }
        else
        {
            rb.rotation = chargeAngle;
        }

        if(Vector2.Distance(transform.position, player.transform.position) > attackRange && !canCharge)
        {
            rb.MovePosition((Vector2)transform.position + (Vector2)(direction * speed * Time.deltaTime));   
        }

        else
        {
            timer -= Time.deltaTime;
        }

        if(timer <= 0 && Vector2.Distance(transform.position, player.transform.position) <= attackRange)
        {
            canCharge = true;
        }

        if(canCharge)
        {
            if(atkCD <= 0)
            {
                hit = false;
                charging = true;
                charge();
                timer = 0.35f;
                atkCD = cdTime;
            }
            else
            {
                atkCD -= Time.deltaTime;
            }
            
            if(Vector2.Distance(transform.position, player.transform.position) > attackRange + 1)
            {
                canCharge = false;
                charging = false;
                speed = 4.3f;
                rb.velocity = Vector2.zero;
            }
           
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(canCharge && hit == false){
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            canCharge = false;
            charging = false;
            hit = true;
        }
    }

    void charge()
    {  
        speed = 9f;
        if(InterceptionDirection(a: player.transform.position, b: transform.position, vA: player.gameObject.GetComponent<Rigidbody2D>().velocity, speed, result: out var chargeDirection))
        {
            if(canCharge)
            {
                charging = true;
                chargeAngle = MyMath.Angle(chargeDirection);
                rb.velocity = chargeDirection * speed;
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

    public static float Angle(Vector2 vector2)
        {
            if (vector2.x < 0)
            {
                return 360 - (Mathf.Atan2(vector2.x, vector2.y) * Mathf.Rad2Deg * -1);
            }
            else
            {
                return Mathf.Atan2(vector2.x, vector2.y) * Mathf.Rad2Deg;
            }
        }
}
