using UnityEngine;

public class PlayerBomb : MonoBehaviour
{
    public Transform Explosion;
    public Gradient gradient;

    public int damage;
    public float splashRange;
    public float activateTimer;

    private float timer;
    private bool inflict;

    void Start()
    {
        Invoke("Despawn", activateTimer + 0.05f);
        timer = 0;
        inflict = false;
    }

    
    void Update()
    {
        gameObject.GetComponent<SpriteRenderer>().color = gradient.Evaluate(timer/activateTimer);
        timer += Time.deltaTime;
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
