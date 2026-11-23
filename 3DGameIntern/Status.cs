using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[DefaultExecutionOrder(-800)]
public class Status : MonoBehaviour
{
    public static Status instance {get; set;}
    [SerializeField] Transform player;
    [SerializeField] Transform enemy;
    [SerializeField] Slider playerHealthBar;
    [SerializeField] Slider playerStaminaBar;
    [SerializeField] Slider bossHealthBar;
    [SerializeField] float increaseStamina;
    [SerializeField] float decreaseStamina;
    [SerializeField] NavMeshAgent agent;
    public bool bossIsDeath = false;


    public bool hurt = false;

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
    }

    private void Start()
    {
        bossHealthBar.gameObject.SetActive(false);
    }
    private void Update()
    {
        BossHealth();

    }


    public void PlayerHealth(int power)
    {
        if (hurt && !PlayerAnim.instance.checkParry)
        {
            playerHealthBar.value -= power;
            hurt = false;

        }
    }

    public void DecreasePlayerStamina()
    {
        playerStaminaBar.value -= 20f * Time.deltaTime;
    }
    public void IncreasePlayerStamina()
    {
        playerStaminaBar.value += 10f * Time.deltaTime;
    }

    private void BossHealth()
    {
        if (!bossIsDeath)
        {
            if (Vector3.Distance(enemy.position, player.position) < 10f)
            {
                bossHealthBar.gameObject.SetActive(true);
            }
            else
            {
                bossHealthBar.gameObject.SetActive(false);
            }
        }


        if(bossHealthBar.value <= 0f && !bossIsDeath)
        {
            bossIsDeath = true;
            PlayerAnim.instance.EnemyPlayAnimAnyState("Death");
            bossHealthBar.gameObject.SetActive(false);
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(player.position, enemy.position);
    }

    public void SetBossHealthBar(int dealDamage)
    {
        if(Vector3.Distance(enemy.position, player.position) <=  agent.stoppingDistance)
        {
            bossHealthBar.value -= dealDamage;
        }
    }

    
}
