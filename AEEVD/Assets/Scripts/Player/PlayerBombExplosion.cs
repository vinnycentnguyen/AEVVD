using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBombExplosion : MonoBehaviour
{
    
    void Start()
    {
        Invoke("Despawn", 0.5f);
    }

    void Despawn()
    {
        Destroy(gameObject);
    }
}
