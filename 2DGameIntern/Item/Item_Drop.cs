using UnityEngine;

public class Item_Drop : MonoBehaviour
{
    [SerializeField] public string itemToDropName;
    [SerializeField] private float yPos;
    public void OnButtonClick()
    {
        dropItemManage(true, itemToDropName);
    }
    public void dropItemManage(bool instantiate, string itemName)
    {
        if (instantiate)
        {
            Vector3 dropPos = PlayerController.playerPosRef.position;


            GameObject itemToDrop = Instantiate(Resources.Load<GameObject>("Potion_Prefabs/" + itemName), new Vector3(dropPos.x, dropPos.y + yPos, dropPos.z), Quaternion.identity);

        }


        if (itemName == "RedPotion")
        {
            check_valueStack(itemName, Count_Item.redPotionCount);
        }
        else if (itemName == "GreenPotion")
        {
            check_valueStack(itemName, Count_Item.greenPotionCount);

        }
        else if (itemName == "BluePotion")
        {
            check_valueStack(itemName, Count_Item.BluePotionCount);

        }
        else if (itemName == "SmallRedPotion")
        {
            check_valueStack(itemName, Count_Item.smallRedPotionCount);

        }
        else if (itemName == "SmallGreenPotion")
        {
            check_valueStack(itemName, Count_Item.smallGreenPotionCount);

        }
        else if (itemToDropName == "SmallBluePotion")
        {
            check_valueStack(itemName, Count_Item.smallBluePotionCount);

        }
    }
    


    private void check_valueStack(string itemName, int value_Stack)
    {
        InventoryManage.countItem(itemName, "minus");
        if (value_Stack > 1)
        {
            Item_Stack.set_item_stack(itemToDropName);
        }
        else if (value_Stack <= 1)
        {
            Destroy(gameObject);
        }
    }
}
