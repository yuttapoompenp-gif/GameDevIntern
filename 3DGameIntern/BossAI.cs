using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[DefaultExecutionOrder(-600)]

public class BossAI : MonoBehaviour
{
    public static BossAI instance { get; set;}
    [SerializeField] public NavMeshAgent bossAgent;
    [SerializeField] Transform boss;
    [SerializeField] Transform player;
    private bool isAttack = false;
    private int currentRandomAttack;
    public bool checkWaitForAttack = false;
    private bool attackPlayer = false;
    public float originalSpeed;
    private float time_count = 0f;
    public bool dealDamage = true;
    public bool enemyAttack = false;
    public bool parryTime = false;
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

        originalSpeed = bossAgent.speed;
    }

    void Update()
    {
        if (!isAttack && !Status.instance.bossIsDeath) 
        {
            StartCoroutine(OnAttack());
        }
    }

    private IEnumerator OnAttack()
    {
        isAttack = true;
        attack_Player();
        if (attackPlayer && dealDamage)
        {
            int randomAttack = Random.Range(1,5);

            checkWaitForAttack = true;
            if(randomAttack != currentRandomAttack)
            {
                PlayerAnim.instance.enemyIdle = false;
                switch (randomAttack)
                {
                    case 1:
                        Debug.Log(randomAttack);

                        PlayerAnim.instance.EnemyPlayAnim("atk1");
                        enemyAttack = true;
                        bossAgent.speed = 0f;

                        while (time_count < 1.03f)
                        {
                            if (PlayerAnim.instance.hit)
                            {
                                PlayerAnim.instance.hit = false;
                                dealDamage = false;
                                break;
                            }

                            time_count += Time.deltaTime;

                            if(time_count >= 1.03)
                            {
                                parryTime = true;
                            }

                            yield return null;

                        }
                        parryTime = false;
                        time_count = 0f;

                        if (dealDamage)
                        {
                            attack_Player();
                            if (attackPlayer)
                            {
                                Status.instance.hurt = true;
                                Status.instance.PlayerHealth(5);
                            }
                        }
                        dealDamage = true;

                        yield return new WaitForSeconds(2.14f);
                        enemyAttack = false;
                        bossAgent.speed = originalSpeed;
                        break;
                    case 2:
                        Debug.Log(randomAttack);

                        PlayerAnim.instance.EnemyPlayAnim("atk2");
                        enemyAttack = true;
                        bossAgent.speed = 0f;

                        while (time_count < 1.03f)
                        {

                            if (PlayerAnim.instance.hit)
                            {
                                PlayerAnim.instance.hit = false;
                                dealDamage = false;
                                break;
                            }

                            time_count += Time.deltaTime;

                            if (time_count >= 1.03)
                            {
                                parryTime = true;
                            }

                            yield return null;
                        }

                        parryTime = false;
                        time_count = 0f;

                        if (dealDamage)
                        {
                            attack_Player();
                            if (attackPlayer)
                            {
                                Status.instance.hurt = true;
                                Status.instance.PlayerHealth(5);
                            }
                        }

                        if (dealDamage)
                        {
                            while (time_count < 1.64f)
                            {

                                if (PlayerAnim.instance.hit)
                                {
                                    PlayerAnim.instance.hit = false;
                                    dealDamage = false;
                                    break;
                                }

                                time_count += Time.deltaTime;

                                if (time_count >= 1.64)
                                {
                                    parryTime = true;
                                }

                                yield return null;
                            }

                            parryTime = false;

                            time_count = 0f;

                            if (dealDamage)
                            {
                                attack_Player();
                                if (attackPlayer)
                                {
                                    Status.instance.hurt = true;
                                    Status.instance.PlayerHealth(10);
                                }
                            }


                        }
                        yield return new WaitForSeconds(1.53f);
                        enemyAttack = false;
                        bossAgent.speed = originalSpeed;
                        dealDamage = true;



                        break;
                    case 3:
                        Debug.Log(randomAttack);

                        PlayerAnim.instance.EnemyPlayAnim("atk3");
                        enemyAttack = true;
                        bossAgent.speed = 0f;

                        while (time_count < 1f)
                        {

                            if (PlayerAnim.instance.hit)
                            {
                                PlayerAnim.instance.hit = false;
                                dealDamage = false;
                                break;
                            }
                            time_count += Time.deltaTime;

                            if (time_count >= 1)
                            {
                                parryTime = true;
                            }

                            yield return null;
                        }

                        parryTime = false;

                        time_count = 0f;

                        if (dealDamage)
                        {
                            attack_Player();
                            if (attackPlayer)
                            {
                                Status.instance.hurt = true;
                                Status.instance.PlayerHealth(5);
                            }
                        }

                        if (dealDamage)
                        {
                            while (time_count < .6f)
                            {
                                if (PlayerAnim.instance.hit)
                                {
                                    PlayerAnim.instance.hit = false;
                                    dealDamage = false;
                                    break;
                                }
                                time_count += Time.deltaTime;

                                if (time_count >= .6f)
                                {
                                    parryTime = true;
                                }

                                yield return null;
                            }
                            parryTime = false;

                            time_count = 0f;

                            if (dealDamage)
                            {
                                attack_Player();
                                if (attackPlayer)
                                {
                                    Status.instance.hurt = true;
                                    Status.instance.PlayerHealth(5);
                                }
                            }
                        }
                        yield return new WaitForSeconds(1.13f);
                        enemyAttack = false;
                        bossAgent.speed = originalSpeed;
                        dealDamage = true;

                        break;
                    case 4:
                        Debug.Log(randomAttack);

                        PlayerAnim.instance.EnemyPlayAnim("atk4");
                        enemyAttack = true;
                        bossAgent.speed = 0f;

                        while (time_count < 1.1f)
                        {

                            if (PlayerAnim.instance.hit)
                            {
                                PlayerAnim.instance.hit = false;
                                dealDamage = false;
                                break;
                            }
                            time_count += Time.deltaTime;

                            if (time_count >= 1.1f)
                            {
                                parryTime = true;
                            }

                            yield return null;
                        }

                        parryTime = false;

                        time_count = 0f;

                        if (dealDamage)
                        {
                            attack_Player();
                            if (attackPlayer)
                            {
                                Status.instance.hurt = true;
                                Status.instance.PlayerHealth(10);
                            }
                        }
                        yield return new WaitForSeconds(1.53f);
                        enemyAttack = false;
                        bossAgent.speed = originalSpeed;
                        dealDamage = true;

                        break;
                }
                PlayerAnim.instance.enemyIdle = true;
                currentRandomAttack = randomAttack;

            }
            checkWaitForAttack = false;
            bossAgent.speed = originalSpeed;
        }
        isAttack = false;

        // yield return new WaitForSeconds(1f);
        PlayerAnim.instance.enemyIsAttack = false;


    }

    private void attack_Player()
    {
        if (!bossAgent.pathPending && bossAgent.hasPath && bossAgent.remainingDistance <= bossAgent.stoppingDistance) attackPlayer = true;
        else
        {
            attackPlayer = false;
        }
    }
}
