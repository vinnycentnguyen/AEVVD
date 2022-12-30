using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject playerBomb;
    public Slider dashSlider;

    private Vector2 moveDirection;

    public float moveSpeed;
    public float dashSpeed;
    public float dashLength = .5f, dashCD = 7f;

    private float activeMoveSpeed;
    private float dashTimer;
    private float dashCDTimer;


    void Start()
    {
        activeMoveSpeed = moveSpeed;
    }

    void Update()
    {
        ProcessInputs();
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(dashCDTimer <= 0 && dashTimer <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashTimer = dashLength;
                Instantiate(playerBomb, transform.position, transform.rotation);
            }
        }

        if(dashTimer > 0)
        {
            dashTimer -= Time.deltaTime;

            if(dashTimer <= 0)
            {
                activeMoveSpeed = moveSpeed;
                dashCDTimer = dashCD;
            }
        }

        if(dashCDTimer > 0)
        {
            dashCDTimer -= Time.deltaTime;
        }

        dashSlider.value = 1 - dashCDTimer/dashCD;
    }

    void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * activeMoveSpeed, moveDirection.y * activeMoveSpeed);
    }
}
