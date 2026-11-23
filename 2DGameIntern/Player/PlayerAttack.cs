using UnityEngine;

[DefaultExecutionOrder(200)]
public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack instance { get; private set; }
    public bool isAttacking;
    public bool countHit;
    public bool holdAttack;
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    private void Update()
    {
        if(isAttacking == false && !holdAttack)
        {
            BasicAttack();
        }

    }

    private void BasicAttack()
    {

        if (Input.GetKeyDown(KeyCode.Mouse1) && !PlayerAnim.instance.slash)
        {
            StartCoroutine(PlayerAnim.instance.PlayAnimTime("Slash"));
            isAttacking = true;
            countHit = true;
            holdAttack = true;
            StartCoroutine(PlayerAnim.instance.HoldTime("Slash"));
        }


        if (Input.GetKeyDown(KeyCode.Mouse0) && !PlayerAnim.instance.slash1)
        {
            StartCoroutine(PlayerAnim.instance.PlayAnimTime("Slash1"));
            isAttacking = true;
            countHit = true;
            holdAttack = true;
            StartCoroutine(PlayerAnim.instance.HoldTime("Slash1"));
        }

        countHit = false;



    }


}