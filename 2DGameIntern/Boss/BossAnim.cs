using UnityEngine;
using System.Collections;
[DefaultExecutionOrder(500)]

public class BossAnim : MonoBehaviour
{
    [SerializeField] Animator bossAnim;
    EnemyTriggered triggered;
    BossStatus bossStatus;
    bool bossAttack = false;
    public static bool bossRot = false;
    bool bossIsDeath = false;
    public  bool bossHandDown = false;
    private Transform boss;
    [SerializeField] Transform player;
    [SerializeField] string monsterName;
    EnemyAttack enemyAttack;
    
    private void Awake()
    {
        triggered = GetComponent<EnemyTriggered>();
        bossStatus = GetComponent<BossStatus>();
        boss = gameObject.transform;
        enemyAttack = GetComponent<EnemyAttack>();
    }

    private void Update()
    {
        if (triggered != null)
        {
            if (triggered.triggered)
            {
                if(bossStatus != null)
                {
                    if (!bossStatus.bossIsDeath && !bossAttack)
                    {

                        if(monsterName == "Boss")
                        {

                            int randomNumber = Random.Range(0, 5);

                            switch (randomNumber)
                            {
                                case 0:
                                    StartCoroutine(EnemyPlayAnim("HandDown"));
                                    break;
                                case 1:
                                    StartCoroutine(EnemyPlayAnim("HandDown"));
                                    break;
                                case 2:
                                    StartCoroutine(EnemyPlayAnim("Slash"));
                                    break;
                                case 3:
                                    StartCoroutine(EnemyPlayAnim("Slash"));
                                    break;
                                case 4:
                                    StartCoroutine(EnemyPlayAnim("Slash"));
                                    break;
                            }
                        }
                        else
                        {
                            StartCoroutine(EnemyPlayAnim("Slash"));
                        }


                    }
                }
            }
        }




        if (bossStatus != null && bossStatus.bossIsDeath && !bossIsDeath)
        {
            StartCoroutine(EnemyPlayAnim("Death"));
        }

        if(Vector3.Distance(boss.position, player.position) > 10)
        {
            StartCoroutine(EnemyPlayAnim("Idle"));
        }
        else if(Vector3.Distance(boss.position, player.position) <= 10 && !triggered.triggered)
        {
            StartCoroutine(EnemyPlayAnim("Walk"));
        }
        else if (triggered.triggered)
        {
            StartCoroutine(EnemyPlayAnim("Idle"));
        }


    }

    private IEnumerator EnemyPlayAnim(string nameAnim)
    {
        if(bossAnim != null)
        {

            if (nameAnim == "HandDown")
            {
                BossController.currentSpeed = 0f;
                bossRot = false;
                bossAttack = true;
                bossAnim.SetTrigger(nameAnim);
                bossAnim.SetTrigger("Idle");
                yield return new WaitForSeconds(1f);
                bossHandDown = true;

                yield return new WaitForSeconds(2f);
                bossRot = true;
                BossController.currentSpeed = BossController.speed;
                //enemyAttack.monsterAttack(monsterName);

                yield return new WaitForSeconds(2f);
                bossAttack = false;
                
            }

            if (nameAnim == "Slash")
            {
                BossController.currentSpeed = 0f;
                bossRot = false;
                bossAttack = true;
                bossAnim.SetTrigger(nameAnim);
                bossAnim.SetTrigger("Idle");
                yield return new WaitForSeconds(1.5f);
                bossRot = true;
                BossController.currentSpeed = BossController.speed;
                enemyAttack.monsterAttack(monsterName);

                yield return new WaitForSeconds(2f);
                bossAttack = false;
            }
            if (nameAnim == "Death")
            {

                bossAnim.SetTrigger("Death");
                bossIsDeath = true;

                yield return new WaitForSeconds(0.52f);

                bossAnim.gameObject.SetActive(false);
            }

            if (nameAnim == "Idle")
            {
                bossAnim.SetTrigger("Idle");
            }

            if (nameAnim == "Walk")
            {
                bossAnim.SetBool("Walk", true);
            }
            else
            {
                bossAnim.SetBool("Walk", false);
            }
        }

    }




}
