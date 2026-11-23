using UnityEngine;
using System.Collections;

[DefaultExecutionOrder(200)]
public class EnemyHitColor : MonoBehaviour
{
    [SerializeField] public SpriteRenderer spriteRenderer;
    EnemyTriggered triggered;
    public bool dealDamage;
    public bool checkHit = false;
    public bool checkPush;

    private Color originalColor;
    private void Awake()
    {
        triggered = GetComponent<EnemyTriggered>();
        originalColor = spriteRenderer.color;
        
    }

    private void Update()
    {

        if (PlayerAttack.instance.isAttacking && triggered != null && triggered.triggered)
        {
            if (!checkHit)
            {
                StartCoroutine(Hit());
            }
        }
    }

    private IEnumerator Hit()
    {

        if (PlayerAnim.instance.slash && !checkHit)
        {
            checkHit = true;
            yield return new WaitForSeconds(0.52f);
            spriteRenderer.color = Color.red;

            dealDamage = true;
            checkPush = true;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = originalColor;




        }
        else if (PlayerAnim.instance.slash1 && !checkHit)
        {
            checkHit = true;
            yield return new WaitForSeconds(0.35f);
            spriteRenderer.color = Color.red;
            dealDamage = true;
            checkPush = true;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = originalColor;



        }

        checkHit = false;

    }





}

