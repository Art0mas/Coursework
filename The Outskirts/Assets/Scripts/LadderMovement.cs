using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    [SerializeField] private float climbSpeed = 5f;

    private float verticalInput;
    private bool isNearLadder = false;
    private bool isClimbing = false;

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

        if (isNearLadder && Mathf.Abs(verticalInput) > 0.01f)
        {
            isClimbing = true;
        }
    }

    private void FixedUpdate()
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isNearLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isNearLadder = false;
            isClimbing = false;
        }
    }
}