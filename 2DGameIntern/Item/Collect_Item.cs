using UnityEngine;


public class Collect_Item : MonoBehaviour
{
    ItemTrigger itemTrigger;
    public string itemName;
    public static GameObject item;
    private void Awake()
    {
        itemTrigger = GetComponent<ItemTrigger>();
    }
    void Update()
    {
        if(itemTrigger.itemTriggered)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                
                item = itemTrigger.item;

                InventoryManage.Collect_item(itemName);

                itemTrigger.itemTriggered = false;
                ItemTrigger.collect_Item = false;

            }


        }
    }

}
