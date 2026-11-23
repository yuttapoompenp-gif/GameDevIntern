using UnityEngine;
[DefaultExecutionOrder(500)]
public class PlayerTriggered : MonoBehaviour
{
    public static PlayerTriggered instance { get; private set; }


    public bool foundMonster;
    public bool isGrounded;
    public bool HandDown;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Monster"))
        {
            foundMonster = true;
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("HandDown"))
        {
            HandDown = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            foundMonster = true;
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("HandDown"))
        {
            HandDown = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            foundMonster = false;
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }

        if (collision.gameObject.CompareTag("HandDown"))
        {
            HandDown = false;
        }
    }






}
