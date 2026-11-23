using UnityEngine;

[DefaultExecutionOrder(500)]
public class PushHit : MonoBehaviour
{
    [SerializeField] Transform obj1;
    [SerializeField] float pushValue = 0.5f;

    EnemyTriggered enemyTriggered;
    EnemyHitColor enemyHitColor;
    private void Awake()
    {
        enemyTriggered = GetComponent<EnemyTriggered>();
        enemyHitColor = GetComponent<EnemyHitColor>();
    }

    void Update()
    {
        if (obj1 != null && enemyHitColor != null && enemyTriggered != null && enemyHitColor.checkPush)
        {
            pushHit();

        }
    }

    private void pushHit()
    {

        if (enemyTriggered.triggered && obj1.localScale.x > 0)
        {
            Vector3 targetPos = new Vector3(obj1.position.x + pushValue, obj1.position.y, 0);

            obj1.position = targetPos;

            enemyHitColor.checkPush = false;
        }
        if (enemyTriggered.triggered && obj1.localScale.x < 0)
        {
            Vector3 targetPos = new Vector3(obj1.position.x - pushValue, obj1.position.y, 0);

            obj1.position = targetPos;

            enemyHitColor.checkPush = false;


        }



    }
}
