using UnityEngine;

public class PlayerBomb : MonoBehaviour
{
    public Transform Explosion;
    public Gradient gradient;

    private Rigidbody2D rb;

    public int damage;
    public float splashRange;
    public float activateTimer;
    public float speed;

    private float timer;
    private float slowFactor = 1f;
    private bool inflict;
    private float spinFactor = 2f;

    void Start()
    {
        Invoke("Despawn", activateTimer + 0.05f);
        rb = this.GetComponent<Rigidbody2D>();
        timer = 0;
        inflict = false;
        rb.velocity = transform.up * speed;
    }

    
    void Update()
    {
        gameObject.GetComponent<SpriteRenderer>().color = gradient.Evaluate(timer/activateTimer);
        timer += Time.deltaTime;
        rb.velocity *= (slowFactor - Time.deltaTime * 2);
        spinFactor -= Time.deltaTime * 0.15f;
        rb.rotation += (1.5f - spinFactor);
        if(timer >= activateTimer)
        {
            inflict = true;
        }

        if(inflict){
            timer = 0;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, splashRange, 640);
            for(int i = 0; i < colliders.Length; i++)
            {
                colliders[i].gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
                inflict = false;
            }
        }
    }

    void Despawn()
    {
        Instantiate(Explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
