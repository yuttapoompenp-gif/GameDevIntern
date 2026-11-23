using UnityEngine;

public class Count_Item : MonoBehaviour
{
    public static int redPotionCount;
    public static int smallRedPotionCount;
    public static int greenPotionCount;
    public static int smallGreenPotionCount;
    public static int BluePotionCount;
    public static int smallBluePotionCount;

    private void Awake()
    {
        redPotionCount = 0; smallRedPotionCount = 0; greenPotionCount = 0; smallGreenPotionCount = 0; BluePotionCount = 0; smallBluePotionCount = 0;
    }


}
