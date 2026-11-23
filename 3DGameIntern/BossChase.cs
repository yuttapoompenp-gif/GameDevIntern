    using UnityEngine;
using UnityEngine.AI;
[DefaultExecutionOrder(-700)]

public class BossChase : MonoBehaviour
{

    [Header("Target")]
    [SerializeField] private Transform playerTarget;
    [SerializeField] private Transform enemy;
    [SerializeField] NavMeshAgent agent;


    private void Awake()
    {

    }


    private void Update()
    {
        if (!Status.instance.bossIsDeath)
        {
            if (Vector3.Distance(enemy.position, playerTarget.position) < 10)
            {
                agent.SetDestination(playerTarget.position);
                BossAI.instance.dealDamage = true;
            }
            else
            {
                agent.speed = 0f;
                BossAI.instance.dealDamage = false;
                if (!BossAI.instance.bossAgent.pathPending && BossAI.instance.bossAgent.hasPath && BossAI.instance.bossAgent.remainingDistance <= BossAI.instance.bossAgent.stoppingDistance)
                {
                    PlayerAnim.instance.EnemyPlayAnim("Idle");
                }
            }

            if (!BossAI.instance.enemyAttack)
            {
                agent.speed = BossAI.instance.originalSpeed;
            }
            else
            {
                agent.speed = 0f;
            }
        }
        



    }
}