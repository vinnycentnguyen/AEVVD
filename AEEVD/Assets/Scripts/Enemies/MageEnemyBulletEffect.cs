using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageEnemyBulletEffect : MonoBehaviour
{
    void Start()
    {
        Invoke("despawn", 0.2f);
    }

    void despawn()
    {
        Destroy(gameObject);
    }

}
