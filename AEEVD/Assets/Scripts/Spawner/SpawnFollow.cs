using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFollow : MonoBehaviour
{
    public GameObject player;
    Vector2 direction;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        direction = player.transform.position - transform.position;
        transform.Translate(direction);
    }
}
