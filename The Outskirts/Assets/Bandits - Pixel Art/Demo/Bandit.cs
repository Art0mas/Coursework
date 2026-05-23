using UnityEngine;
using System.Collections;

public class Bandit : MonoBehaviour {

    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 7f;
    [SerializeField] private float jumpForce = 7f;


    private Animator anim;
    private Rigidbody2D rb;
    private Sensor_Bandit groundSensor;
    private bool isGrounded = false;
    private bool combatIdle = false;
    private bool isDead = false;
    private float currentSpeed;

    void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
        currentSpeed = walkSpeed;
    }
	
	void Update () {
        if (!isGrounded && groundSensor.State()) {
            isGrounded = true;
            anim.SetBool("Grounded", isGrounded);
        }

        if(isGrounded && !groundSensor.State()) {
            isGrounded = false;
            anim.SetBool("Grounded", isGrounded);
        }

        float inputX = Input.GetAxis("Horizontal");

        if (inputX > 0)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (inputX < 0)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        rb.velocity = new Vector2(inputX * currentSpeed, rb.velocity.y);
        anim.SetFloat("AirSpeed", rb.velocity.y);

        if (Input.GetKey(KeyCode.LeftShift))
            currentSpeed = sprintSpeed;
        else

        if (Input.GetKeyDown(KeyCode.E)) {
            if(!isDead)
                anim.SetTrigger("Death");
            else
                anim.SetTrigger("Recover");

            isDead = !isDead;
        }
            
        //Hurt
        else if (Input.GetKeyDown("q"))
            anim.SetTrigger("Hurt");

        //Attack
        else if(Input.GetMouseButtonDown(0)) {
            anim.SetTrigger("Attack");
        }

        //Change between idle and combat idle
        else if (Input.GetKeyDown("f"))
            combatIdle = !combatIdle;

        //Jump
        else if (Input.GetButtonDown("Jump") && isGrounded) {
            anim.SetTrigger("Jump");
            isGrounded = false;
            anim.SetBool("Grounded", isGrounded);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            groundSensor.Disable(0.2f);
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
            anim.SetInteger("AnimState", 2);

        //Combat Idle
        else if (combatIdle)
            anim.SetInteger("AnimState", 1);

        //Idle
        else
            anim.SetInteger("AnimState", 0);
    }
}
