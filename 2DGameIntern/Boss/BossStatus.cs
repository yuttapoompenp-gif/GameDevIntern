using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

[DefaultExecutionOrder(400)]
public class BossStatus : MonoBehaviour
{

    [SerializeField] Transform player;
    [SerializeField] Transform boss;

    [SerializeField] public Slider bossHealthBar;

    [SerializeField] public int maxDistanceTriggerBoss = 10;
    EnemyTriggered triggered;
    EnemyHitColor enemyHitColor;


    public bool bossIsDeath;

    private void Awake()
    {

        triggered = GetComponent<EnemyTriggered>();
        enemyHitColor = GetComponent<EnemyHitColor>();
    }

    private void Start()
    {
        bossHealthBar.gameObject.SetActive(false);
    }

    void Update()
    {
        if (bossHealthBar.value <= 0 && !bossIsDeath)
        {
            bossIsDeath = true;
        }

        if (enemyHitColor != null && enemyHitColor.dealDamage)
        {
            if (triggered != null)
            {
                if (triggered.triggered)
                {
                    if (PlayerAnim.instance.slash)
                    {
                        Hit(20);
                        enemyHitColor.dealDamage = false;

                    }
                    else if (PlayerAnim.instance.slash1)
                    {
                        Hit(10);
                        enemyHitColor.dealDamage = false;

                    }
                }
            }

        }
        if (boss != null)
        {
            Trigger_HPBar();
        }


    }

    public void Trigger_HPBar()
    {
        if (Vector3.Distance(player.position, boss.position) <= maxDistanceTriggerBoss)
        {
            bossHealthBar.gameObject.SetActive(true);
        }
        else
        {
            bossHealthBar.gameObject.SetActive(false);
        }
    }



    public void Hit(int dealDamage)
    {
        bossHealthBar.value -= dealDamage;
    }

}
