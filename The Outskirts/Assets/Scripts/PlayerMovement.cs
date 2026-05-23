using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 6f;
    [SerializeField] private float sprintSpeed = 9f;
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask whatIsGround;

    [SerializeField] private AudioSource walkAudio;
    [SerializeField] private AudioSource sprintAudio;

    [SerializeField] private Animator anim;

    private Rigidbody2D rb;
    private float moveInput;
    private bool isGrounded;
    private float currentSpeed;
    private bool isRun = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        walkAudio.Pause();
        sprintAudio.Pause();
    }

    void Update()
    {
        rb.velocity = new Vector2(moveInput * currentSpeed, rb.velocity.y);
        Move();
        Jump();

    }
    private void Move()
    {
        moveInput = Input.GetAxis("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        
        if (!walkAudio.isPlaying) walkAudio.Play();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;
            isRun = true;

            if (!sprintAudio.isPlaying)
            {
                walkAudio.Pause();
                sprintAudio.Play();
            }
            
        }
        else
        {
            currentSpeed = walkSpeed;
            isRun = false;

            if (sprintAudio.isPlaying)
            {
                sprintAudio.Pause();
                walkAudio.Play();
            }
            
        }
        anim.SetFloat("Speed", Mathf.Abs(moveInput));
        anim.SetBool("IsRun", isRun);
        anim.SetBool("IsGround", isGrounded);

    }
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            anim.SetBool("IsJump", true);
        }
    }
}
