using System.Collections.Generic;
using UnityEngine;

public class InventoryManage : MonoBehaviour
{
    [SerializeField] private  List<GameObject> slotList = new List<GameObject>();
    [SerializeField] public static  List<GameObject> slotList_sta = new List<GameObject>();

    static GameObject itemToAdd;
    public static string item_Name;


    private void Awake()
    {
        slotList_sta = slotList;
    }

    void Update()
    {
        
    }

    private static GameObject check_empty_slot()
    {
        for (int i = 0; i < slotList_sta.Count; i++)
        {
            if (slotList_sta[i].transform.childCount == 0)
            {
                return slotList_sta[i];
            }

        }
        return null;
    }

    private static bool check_repeat_item(string itemName)
    {
        bool repeat;
        List<GameObject> slotList_sta_ls = new List<GameObject>();

        for (int i = 0; i < slotList_sta.Count; i++)
        {
            if(slotList_sta[i].transform.childCount > 0)
            {

                slotList_sta_ls.Add(slotList_sta[i].transform.GetChild(0).gameObject);
            }
        }

        foreach (var slot in slotList_sta_ls)
        {
            if (slot.CompareTag(itemName))
            {
                repeat = true;
                return repeat;

            }
        }

        repeat = false;
        return repeat;
    }

    public static void countItem(string itemName, string Condition)
    {
        if (Condition == "plus")
        {
            if (itemName == "RedPotion")
            {
                Count_Item.redPotionCount++;
            }
            else if (itemName == "SmallRedPotion")
            {
                Count_Item.smallRedPotionCount++;
            }
            else if (itemName == "GreenPotion")
            {
                Count_Item.greenPotionCount++;
            }
            else if (itemName == "SmallGreenPotion")
            {
                Count_Item.smallGreenPotionCount++;
            }
            else if (itemName == "BluePotion")
            {
                Count_Item.BluePotionCount++;
            }
            else if (itemName == "SmallBluePotion")
            {
                Count_Item.smallBluePotionCount++;
            }
        }
    
        if (Condition == "minus")
        {
            if (itemName == "RedPotion")
            {
                Count_Item.redPotionCount--;
            }
            else if (itemName == "SmallRedPotion")
            {
                Count_Item.smallRedPotionCount--;
            }
            else if (itemName == "GreenPotion")
            {
                Count_Item.greenPotionCount--;
            }
            else if (itemName == "SmallGreenPotion")
            {
                Count_Item.smallGreenPotionCount--;
            }
            else if (itemName == "BluePotion")
            {
                Count_Item.BluePotionCount--;
            }
            else if (itemName == "SmallBluePotion")
            {
                Count_Item.smallBluePotionCount--;
            }
        }


        Item_Stack.set_item_stack(itemName);

    }



    public static void Collect_item(string itemName)
    {
        GameObject emptySlot = check_empty_slot();
        bool repeat = check_repeat_item(itemName);
        
        if(emptySlot != null && !repeat)
        {
            item_Name = itemName;

            itemToAdd = Instantiate(Resources.Load<GameObject>("Potion_Image/" + itemName), emptySlot.transform.position, Quaternion.identity);
            itemToAdd.transform.SetParent(emptySlot.transform);

            Destroy(Collect_Item.item);

            countItem(itemName, "plus");



        }
        else if (repeat)
        {
            Destroy(Collect_Item.item);

            countItem(itemName, "plus");

        }
    }
}