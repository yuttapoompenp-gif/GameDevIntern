using System.Collections;
using UnityEngine;

[DefaultExecutionOrder(700)]
public class SlimeAnim : MonoBehaviour
{

    [SerializeField] public Animator slimeAnim;
    [SerializeField] public string monsterName;
    EnemyTriggered slimeTrigger;
    SlimeSetActive slimeSetActive;
    public Transform slime;
    public Transform player;
    private bool attack = false;

    EnemyAttack enemyAttack;

    private void Awake()
    {

        slimeTrigger = GetComponent<EnemyTriggered>();
        slimeSetActive = GetComponent<SlimeSetActive>();
        enemyAttack = GetComponent<EnemyAttack>();
    }

    void Update()
    {
        if (slimeSetActive.isDead == false)
        {
            if (slimeTrigger != null)
            {
                if (slimeTrigger.triggered && !attack)
                {
                    StartCoroutine(EnemyPlayAnim("Spin_ATK", monsterName));
                }
                else if (slimeTrigger.triggered)
                {
                    StartCoroutine(EnemyPlayAnim("Slime_donot_Walk", monsterName));
                }
                else if (Vector3.Distance(slime.position, player.position) <= 10f && !slimeTrigger.triggered)
                {
                    StartCoroutine(EnemyPlayAnim("Slime_Walk", monsterName));
                }


            }

            if (Vector3.Distance(slime.position, player.position) > 10f)
            {
                StartCoroutine(EnemyPlayAnim("Slime_donot_Walk", monsterName));
            }


        }

    }





    public IEnumerator EnemyPlayAnim(string nameAnim, string monsterName)
    {
        if (nameAnim == "Spin_ATK")
        {
            attack = true;
            slimeAnim.SetTrigger(nameAnim);
            slimeAnim.SetTrigger("Idle");
            yield return new WaitForSeconds(0.7321429f);
            enemyAttack.monsterAttack(monsterName);


            yield return new WaitForSeconds(1f);

            attack = false;

        }


        if (nameAnim == "Slime_Walk")
        {
            slimeAnim.SetBool("Walk", true);
            yield return 0;
        }

        if (nameAnim == "Slime_donot_Walk")
        {
            slimeAnim.SetBool("Walk", false);
            yield return 0;
        }

    }



}