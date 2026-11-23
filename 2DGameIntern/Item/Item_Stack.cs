using TMPro;
using UnityEngine;

public class Item_Stack : MonoBehaviour
{

    private static TMP_Text stackText;
    private static GameObject potion_Item;

    public static GameObject set_item_stack(string itemName)
    {
        for (int i = 0; i < InventoryManage.slotList_sta.Count; i++)
        {

            foreach (Transform potion in InventoryManage.slotList_sta[i].transform)
            {
                if (potion.gameObject.layer == LayerMask.NameToLayer("Potion"))
                {
                    potion_Item = potion.gameObject;
                }
            }

            GameObject text_part = null;

            foreach (Transform child in potion_Item.transform)
            {
                if (child.tag == "CountStack")
                {
                    text_part = child.gameObject;
                    break;
                }
            }

            if (text_part != null)
            {
                stackText = text_part.GetComponent<TMP_Text>();


                if (potion_Item.transform.CompareTag(itemName))
                {
                    if (itemName == "RedPotion")
                    {
                        stackText.text = Count_Item.redPotionCount.ToString();
                    }
                    else if (itemName == "GreenPotion")
                    {
                        stackText.text = Count_Item.greenPotionCount.ToString();
                    }
                    else if (itemName == "BluePotion")
                    {
                        stackText.text = Count_Item.BluePotionCount.ToString();
                    }
                    else if (itemName == "SmallRedPotion")
                    {
                        stackText.text = Count_Item.smallRedPotionCount.ToString();
                    }
                    else if (itemName == "SmallGreenPotion")
                    {
                        stackText.text = Count_Item.smallGreenPotionCount.ToString();
                    }
                    else if (itemName == "SmallBluePotion")
                    {
                        stackText.text = Count_Item.smallBluePotionCount.ToString();
                    }
                }

            }





        }
        return null;
    }
}
