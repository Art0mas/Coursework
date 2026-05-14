using UnityEngine;

public class EnemyMovement: MonoBehaviour
{
    [SerializeField] private float Y;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float speed;
    [SerializeField] private float pause = 2f;

    private float waitTimer;
    private bool isWaiting = false;
    private bool movingRight = true;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        transform.position = new Vector3(transform.position.x, Y, transform.position.z);
    }

    void Update()
    {
        Vector3 currentPos = transform.position;
        currentPos.y = Y;

        if (isWaiting)
        {
            if (anim != null) anim.SetBool("isRun", false);
            waitTimer -= Time.deltaTime;

            if (waitTimer <= 0)
            {
                isWaiting = false; 
                Flip();           
            }
        }

        else
        {
            if (anim != null) anim.SetBool("isRun", true);

            if (movingRight)
            {
                currentPos.x += speed * Time.deltaTime;
                if (currentPos.x >= maxX)
                {
                    currentPos.x = maxX;
                    movingRight = false; 
                    StartWait();       
                }
            }
            else
            {
                currentPos.x -= speed * Time.deltaTime;

                if (currentPos.x <= minX)
                {
                    currentPos.x = minX;
                    movingRight = true;  
                    StartWait();       
                }
            }
        }

        transform.position = currentPos;
    }

    private void StartWait()
    {
        isWaiting = true;
        waitTimer = pause; 
    }

    private void Flip()
    {
        if (movingRight)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
    }
    

}