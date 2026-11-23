using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance {get; set;}

    private float xAxis;
    [SerializeField] public  float speed = 5f;
    [SerializeField] public  float dashSpeed = 7f;
    [SerializeField] public  float currentSpeed;
    [SerializeField] private Transform player;
    public static Transform playerPosRef;
    [SerializeField] private float jump_Force = 10f;
    [SerializeField] float boost = 1f;

    Rigidbody2D rb;



    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        playerPosRef = player;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1;
        rb.freezeRotation = true;
    }

    void Update()
    {
        if (player == null) return;

        xAxis = Input.GetAxisRaw("Horizontal");


        if (Input.GetKeyDown(KeyCode.Space) && PlayerTriggered.instance.isGrounded)
        {
            Jump();
        }

        Flip();
    }


    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(xAxis * currentSpeed * boost, rb.linearVelocity.y);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jump_Force);
    }

    private void Flip()
    {
        if (xAxis > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (xAxis < 0)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
    }

    public  void speedBoost_long (float multiply)
    {
        boost = boost * multiply;
    }

    public  void speedBoost_short (float multiply)
    {
        boost = boost * multiply;
    }

    public IEnumerator down_speedBoost_TimeCount(float timeToWait)
    {

        yield return new WaitForSeconds(timeToWait);
        down_speedBoost();
    }

    private void down_speedBoost()
    {
        boost = 1;
    }
}
