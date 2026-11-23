using System.Collections;
using UnityEngine;

[DefaultExecutionOrder(300)]
public class PlayerAnim : MonoBehaviour
{
    public static PlayerAnim instance {  get; private set; }
    [SerializeField] public Animator playerAnim;
    public bool slash , slash1 = false;
    public bool jump;



    private void Awake()
    {
        if(instance!= null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

    }

    void Update()
    {
        if (playerAnim == null) return;
        Player_Anim();
    }

    public void Player_Anim()
    {

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            playerAnim.SetBool("Run", true);
        }
        else if (Input.GetAxisRaw("Horizontal") == 0)
        {
            playerAnim.SetBool("Run", false);
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.D))
        {
            playerAnim.SetBool("Dash", true);
            PlayerController.instance.currentSpeed = PlayerController.instance.dashSpeed;
        }
        else
        {
            playerAnim.SetBool("Dash", false);
            PlayerController.instance.currentSpeed = PlayerController.instance.speed;
        }


    }



    public IEnumerator PlayAnimTime(string nameAnim)
    {

        if (nameAnim == "Slash")
        {
            slash = true;

            playerAnim.SetTrigger("Slash");
            
            playerAnim.SetTrigger("Idle");

            yield return null;
            PlayerAttack.instance.isAttacking = false;

            slash = false;


        }

        if (nameAnim == "Slash1")
        {
            slash1 = true;

            playerAnim.SetTrigger("Slash1");

            playerAnim.SetTrigger("Idle");

            yield return null;
            PlayerAttack.instance.isAttacking = false;


            slash1 = false;


        }
    }

    public IEnumerator HoldTime(string nameAnim)
    {
        if (nameAnim == "Slash")
        {
            yield return new WaitForSeconds(1f);
            PlayerAttack.instance.holdAttack = false;

        }

        if (nameAnim == "Slash1")
        {
            yield return new WaitForSeconds(1f);
            PlayerAttack.instance.holdAttack = false;

        }
    }



}