using UnityEngine;
using UnityEngine.AI;

public class summonMonsterChase : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    Vector3 summonMonster;
    GameObject player;
    summonMonsterAnim monsterAnim;
    bool onAttack;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        monsterAnim = GetComponent<summonMonsterAnim>();
        monsterAnim.PlayAnimCrossFade("Idle");


    }


    void Update()
    {
        summonMonster = gameObject.transform.position;
        agent.SetDestination(player.transform.position);



        if (Vector3.Distance(summonMonster, player.transform.position) <= 1.5f)
        {
            monsterAnim.PlayAnimCrossFade("Attack");

        }
        else
        {
            monsterAnim.PlayAnimCrossFade("Run");

        }




    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Debug.Log(Vector3.Distance(player.transform.position, summonMonster));

        Gizmos.DrawLine(player.transform.position, summonMonster);
    }
}
