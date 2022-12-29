using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherEnemyBulletProjectile : MonoBehaviour
{
    public float bulletSpeed;
    private int damage = 1;

    void Start()
    {
        Invoke("DestroyProjectile", 0.75f);
    }

    void FixedUpdate()
    {
        transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        Destroy(gameObject);
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
