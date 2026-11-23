using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private  GameObject monster;
    private EnemyTriggered enemyTriggered;
    private void Awake()
    {
        monster = gameObject;
        enemyTriggered = GetComponent<EnemyTriggered>();
    }


    public void monsterAttack(string monsterName)
    {

        if (monsterName == "Slime")
        {
            slimeAttak();
        }
        else if (monsterName == "Boss" || monsterName == "MiniBoss")
        {
            MiniBossAndBossAttack();
        }
    }

    public  void slimeAttak()
    {
        if (monster.CompareTag("BigSlime"))
        {
            dealDamageToPlayer(10f);
        }
        else if (monster.CompareTag("MediumSlime"))
        {
            dealDamageToPlayer(5f);
        }
        else if (monster.CompareTag("SmallSlime"))
        {
            dealDamageToPlayer(2.5f);
        }
    }

    public  void MiniBossAndBossAttack()
    {
        if (monster.CompareTag("MiniBoss"))
        {
            dealDamageToPlayer(15f);
        }
        else if (monster.CompareTag("Boss"))
        {
            dealDamageToPlayer(30f);
        }
    }

    private void dealDamageToPlayer(float damage_value)
    {
        if (enemyTriggered.triggered)
        {
            if (PlayerStatus.instance.protectBar.value > 0)
            {
                PlayerStatus.instance.dealDamageToPlayerProtect(damage_value);
            }
            else if (PlayerStatus.instance.protectBar.value <= 0)
            {
                PlayerStatus.instance.dealDamageToPlayer(damage_value);
            }
        }

    }
}
