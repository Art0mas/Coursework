using UnityEngine;
using System.Collections;

public class HeroKnight : MonoBehaviour {

    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 7f;
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private bool noBlood = false;

    private Animator anim;
    private Rigidbody2D rb;
    private Sensor_HeroKnight groundSensor;
    private bool isGrounded = false;
    private bool isDead = false; 
    private float delayToIdle = 0.0f;
    private float currentSpeed;

    void Start ()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();

        currentSpeed = walkSpeed;
    }

    void Update ()
    {
        if (isDead) return;

        if (!isGrounded && groundSensor.State())
        {
            isGrounded = true;
            anim.SetBool("Grounded", isGrounded);
        }

        if (isGrounded && !groundSensor.State())
        {
            isGrounded = false;
            anim.SetBool("Grounded", isGrounded);
        }

        float inputX = Input.GetAxis("Horizontal");
        if (inputX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
            
        else if (inputX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        if (Input.GetKey(KeyCode.LeftShift))
            currentSpeed = sprintSpeed;
        else
            currentSpeed = walkSpeed;

        // Move
        rb.velocity = new Vector2(inputX * currentSpeed, rb.velocity.y);
        anim.SetFloat("AirSpeedY", rb.velocity.y);

        //Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            anim.SetTrigger("Jump");
            isGrounded = false;
            anim.SetBool("Grounded", isGrounded);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            groundSensor.Disable(0.2f);
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            delayToIdle = 0.05f;
            anim.SetInteger("AnimState", 1);
        }

        else IdleAnim();

    }
    public void IdleAnim()
    {
        delayToIdle -= Time.deltaTime;

        if (delayToIdle < 0)
            anim.SetInteger("AnimState", 0);
    }

    public void DeathAnim()
    {
        if (!isDead) 
        {
            isDead = true; 
            rb.velocity = Vector2.zero;

            anim.SetBool("noBlood", noBlood);
            anim.SetTrigger("Death");
        }
    }
    public void ResetAnim()
    {
        anim.Rebind();
        anim.Update(0f); 
        isGrounded = true;
        isDead = false;
    }
}
