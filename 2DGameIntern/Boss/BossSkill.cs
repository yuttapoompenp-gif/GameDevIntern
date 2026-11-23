using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq.Expressions;

public class BossSkill : MonoBehaviour
{
    [SerializeField] private List<Animator> bossAnim = new List<Animator>();
    [SerializeField] private List<Collider2D> bossHandDownCollider = new List<Collider2D>();
    [SerializeField] private Collider2D player;
    private BossAnim bossAnim_script;
    [SerializeField] private GameObject boss;
    EnemyAttack enemyAttack;
    EnemyTriggered triggered;
    bool bossHandDownCoolDown = false;



    private void Awake()
    {
        bossAnim_script = boss.GetComponent<BossAnim>();
        Boss_Skill_Collision(false);
        enemyAttack = GetComponent<EnemyAttack>();
        triggered = GetComponent<EnemyTriggered>();

    }

    void Update()
    {
            
        if (bossAnim_script != null && triggered.triggered)
        {
            if (bossAnim_script.bossHandDown && !bossHandDownCoolDown)
            {
                Boss_Skill_Collision(true);
                bossAnim_script.bossHandDown = false;
                Boss_Skill();
                StartCoroutine(HandDown_Attack_Time());


            }

        }

        if (PlayerTriggered.instance.HandDown)
        {
            PlayerTriggered.instance.HandDown = false;      
            enemyAttack.monsterAttack("Boss");

        }
    }

    public void Boss_Skill()
    {
        foreach (var index in bossAnim)
        {
            index.SetTrigger("HandDownAttack");
            index.SetTrigger("IdleHandDown");
        }
    }
    public void Boss_Skill_Collision(bool isActive)
    {

        foreach (var index in bossHandDownCollider)
        {
            index.enabled = isActive;
        }
    }

    private IEnumerator HandDown_Attack_Time()
    {
        bossHandDownCoolDown = true;
        yield return new WaitForSeconds(0.8770492f);
        Boss_Skill_Collision(false);
        bossHandDownCoolDown = false;

    }
}
