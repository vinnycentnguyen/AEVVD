using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyChase : MonoBehaviour
{
    
    public GameObject EnemyBullet;
    public GameObject player;
    public Transform rangedFirePoint;
    public Transform enemyWeapon;

    public float speed;
    private bool inRange;
    public float attackRange;
    public float cdTime;
    private float atkCD;
  
    void Update()
    {
        Vector3 difference = player.transform.position - enemyWeapon.transform.position;
        float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 90 * Time.deltaTime);

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
                Instantiate(EnemyBullet, rangedFirePoint.position, rangedFirePoint.transform.rotation);
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
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }
}
