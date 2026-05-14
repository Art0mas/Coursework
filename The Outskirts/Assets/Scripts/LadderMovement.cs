using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    [Header("Налаштування")]
    public float climbSpeed = 5f;

    private bool isNearLadder = false; 
    private bool isClimbing = false;  

    private float verticalInput;
    private Rigidbody2D rb;
    private float defaultGravity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultGravity = rb.gravityScale;
    }

    void Update()
    {
        verticalInput = Input.GetAxisRaw("Vertical");

        if (isNearLadder && Mathf.Abs(verticalInput) > 0f)
        {
            isClimbing = true;
        }
    }

    void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, verticalInput * climbSpeed);
        }
        else
        {
            rb.gravityScale = defaultGravity;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isNearLadder = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isNearLadder = false;
            isClimbing = false;
        }
    }
}