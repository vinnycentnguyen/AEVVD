using UnityEngine;

public class TerrainEnemyWall : MonoBehaviour
{
    private GameObject player;

    public float freezeTimer;

    void Start()
    {
        Invoke("Despawn", 3.5f);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerMovement>().stopMovement(freezeTimer);
            Invoke("Despawn", 0.25f);
        }
    }

    void Despawn()
    {
        Destroy(gameObject);
    }

}
