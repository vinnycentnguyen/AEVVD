using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    
    public Transform FirePoint;
    public GameObject bulletPrefab;
    public GameObject playerBomb;

    public float firerate;
    public float bombCD;
    public bool holdingDown;

    private float nextFire;
    private float nextBomb;
    

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        if(Input.GetKey(KeyCode.Mouse0))
        {
            holdingDown = true;
            Shoot();
        }
        else
        {
            holdingDown = false;
        }
        if(Input.GetButtonDown("Fire2"))
        {
            ThrowBomb();
        }
    }

    void Shoot()
    {
        if(Time.time > nextFire)
        {
            nextFire = Time.time + firerate;
            Instantiate(bulletPrefab, FirePoint.position, FirePoint.rotation);
        }
    }

    void ThrowBomb()
    {
        if(Time.time > nextBomb)
        {
            nextBomb = Time.time + bombCD;
            Instantiate(playerBomb, FirePoint.position, FirePoint.rotation);
            
        }
    }
}
