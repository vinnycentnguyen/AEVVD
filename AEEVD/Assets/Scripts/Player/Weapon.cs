using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public GameObject bombSlider;
    public Transform FirePoint;
    public GameObject bulletPrefab;
    public GameObject playerBomb;

    

    public float firerate;
    public float bombCD;
    public bool holdingDown;

    private float nextFire;
    private float nextBomb;
    private float bombTimer;
    private float bombCDTimer;

    void Start()
    {
        bombSlider = GameObject.FindGameObjectWithTag("BombSlider");
    }

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
            if(bombCDTimer <= 0 && bombTimer <= 0)
            {
                Instantiate(playerBomb, FirePoint.position, FirePoint.rotation);
                bombCDTimer = bombCD;
            }
        }
        if(bombTimer > 0)
        {
            bombTimer -= Time.deltaTime;

            if(bombTimer <= 0)
            {
                bombCDTimer = bombCD;
            }
        }

        if(bombCDTimer > 0)
        {
            bombCDTimer -= Time.deltaTime;
        }

        bombSlider.transform.GetChild(0).GetComponent<Slider>().value = 1 - bombCDTimer/bombCD;
        Debug.Log(1 - bombCDTimer/bombCD);
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
