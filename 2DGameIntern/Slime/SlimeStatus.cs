using UnityEngine;

public class SlimeStatus : MonoBehaviour
{
    [SerializeField] public float bigSmileHealth = 100f;
    [SerializeField] public float mediumSmileHealth = 50f;
    [SerializeField] public float mediumSmileHealth1 = 50f;
    [SerializeField] public float smallSmileHealth = 25f;
    [SerializeField] public float smallSmileHealth1 = 25f;
    [SerializeField] public float smallSmileHealth2 = 25f;
    [SerializeField] public float smallSmileHealth3 = 25f;

    public GameObject slime;
    EnemyTriggered slimeTrigger;
    EnemyHitColor enemyHitColor;

    private void Awake()
    {
        slimeTrigger = GetComponent<EnemyTriggered>();
        enemyHitColor = GetComponent<EnemyHitColor>();
    }

    private void Update()
    {
        if (enemyHitColor != null && enemyHitColor.dealDamage)
        {
            if (slimeTrigger != null)
            {
                if (slimeTrigger.triggered)
                {
                    Hit();

                    enemyHitColor.dealDamage = false;

                }
            }
        }
    }

    private void Hit()
    {
        if (slime.CompareTag("BigSlime"))
        {
            bigSmileHealth -= 10f;

        }
        else if (slime.CompareTag("MediumSlime"))
        {
            mediumSmileHealth -= 10f;
        }
        else if (slime.CompareTag("SmallSlime"))
        {
            smallSmileHealth -= 10f;
        }
    }
}

