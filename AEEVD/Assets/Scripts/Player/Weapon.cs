using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    
    public Transform FirePoint;
    public GameObject bulletPrefab;
    public float firerate;
    float nextFire;
    public bool holdingDown;

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
        else{
            holdingDown = false;
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
}
