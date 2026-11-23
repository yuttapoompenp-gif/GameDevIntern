using UnityEngine;

public class Potion_Feature : MonoBehaviour
{
    Item_Drop item_Drop;
    string itemName;
    void Awake()
    {
        item_Drop = GetComponent<Item_Drop>();
    }

    private void Start()
    {
        itemName = item_Drop.itemToDropName;
    }

    public void OnButtonClick()
    {
       //เหลือฟังชันคุณสบัติตอนใช้

       //healt
        if(itemName == "RedPotion")
        {
            PlayerStatus.instance.healthPlayer(30f);   
        }
        else if(itemName == "SmallRedPotion")
        {
            PlayerStatus.instance.healthPlayer(15f);
        }
        //protect
        else if(itemName == "BluePotion")
        {
            PlayerController.instance.speedBoost_long(1.5f);
            PlayerController.instance.StartCoroutine(PlayerController.instance.down_speedBoost_TimeCount(5f));

        }
        else if(itemName == "SmallBluePotion")
        {
            PlayerController.instance.speedBoost_short(1.5f);
            PlayerController.instance.StartCoroutine(PlayerController.instance.down_speedBoost_TimeCount(5f));

        }
        //boostspeed
        else if (itemName == "GreenPotion")
        {
            PlayerStatus.instance.healthPlayerProtect(50f);
        }
        else if (itemName == "SmallGreenPotion")
        {
            PlayerStatus.instance.healthPlayerProtect(25f);
        }
        item_Drop.dropItemManage(false, itemName);
    }
}
