using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyBullet : MonoBehaviour
{
    public float bulletSpeed;

    void Start()
    {
        Invoke("DestoryProjectile", 0.7f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime);
    }

    void DestoryProjectile()
    {
        Destroy(gameObject);
    }
}
