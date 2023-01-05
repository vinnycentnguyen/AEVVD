using UnityEngine;

public class TerrainEnemyChase : MonoBehaviour
{
    
    public GameObject Terrain;
    public GameObject player;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector3 direction;
    private Vector2 playerDir;
    private Vector2 addVelocity; 

    public float attackRange;
    public float cdTime;
    public float speed;

    private bool inRange;
    private float atkCD;
    private float angle;
    private float playerAng;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = this.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        direction = player.transform.position - transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;

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
                playerDir = player.GetComponent<Rigidbody2D>().velocity;
                playerAng = Mathf.Atan2(playerDir.x, playerDir.y) * Mathf.Rad2Deg;
                var step = speed * Time.deltaTime;
                addVelocity = new (player.GetComponent<Rigidbody2D>().velocity.x * 2/3, player.GetComponent<Rigidbody2D>().velocity.y * 2/3);
                Instantiate(Terrain, (Vector2)player.transform.position + addVelocity, Quaternion.Inverse(Quaternion.AngleAxis(playerAng, Vector3.forward)));
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
            rb.MovePosition((Vector2)transform.position + (Vector2)(direction * speed * Time.deltaTime));       
        }
    }
}
