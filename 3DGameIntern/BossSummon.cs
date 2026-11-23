using UnityEngine;
using System.Collections.Generic;

public class BossSummon : MonoBehaviour
{
    [SerializeField] List<Vector3> monsterSummon = new List<Vector3>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            monster_Summon();
            Debug.Log("Summon");
        }
    }

    private void monster_Summon()
    {
        foreach (var vector in monsterSummon)
        {
            Instantiate(Resources.Load<GameObject>("summonMonster"), vector, Quaternion.identity);

        }

    }  

}
