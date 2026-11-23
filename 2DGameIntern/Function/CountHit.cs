using UnityEngine;
using TMPro;
public class CountHit : MonoBehaviour
{
    [SerializeField] private TMP_Text countHit;
    int countHitValue = 0;

    void Update()
    {
        if(PlayerAttack.instance != null && PlayerTriggered.instance != null)
        {
            if (countHit != null && PlayerAttack.instance.countHit && PlayerTriggered.instance.foundMonster)
            {
                countHitValue++;
                Count_Hit();
            }
        }
    }

    private void Count_Hit()
    {
        string countHitValueStr = countHitValue.ToString();
        countHit.text = countHitValueStr;

    }
}
