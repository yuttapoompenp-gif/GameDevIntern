using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    [SerializeField] GameObject inventory;
    [SerializeField] List<GameObject> potionList = new List<GameObject>();
    [SerializeField] List<GameObject> backGroundList = new List<GameObject>();


    private bool openInventory;
    public static bool closeBackGround = false;
    public static bool closeBackGround_Check = false;

    void Awake()
    {
        inventory.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Open_inventory();
        }

    }

    public  void Open_inventory()
    {

        if (!openInventory)
        {
            openInventory = true;
            closeBackGround = false;
            closeBackGround_Check = true;
            inventory.SetActive(true);
            potion_List();

            for (int i = 0; i < potionList.Count; i++)
            {
                foreach(Transform background in potionList[i].transform)
                {
                    if (background.CompareTag("BackGround"))
                    {
                        backGroundList.Add(background.gameObject);
                    }
                }
            }




            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
        else if (openInventory)
        {
            openInventory = false;
            closeBackGround = true;
            closeBackGround_Check = true;
            inventory.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            foreach(GameObject close_backGround in backGroundList)
            {
              if (close_backGround != null)
                {
                    close_backGround.SetActive(false);
                }

            }
            
            potionList.Clear();
            backGroundList.Clear();
        }
    }

    public void potion_List()
    {
        for (int i = 0; i < InventoryManage.slotList_sta.Count; i++)
        {
            foreach (Transform potion in InventoryManage.slotList_sta[i].transform)
            {
                if (potion.gameObject.layer == LayerMask.NameToLayer("Potion"))
                {
                    potionList.Add(potion.gameObject);
                }
            }
        }
    }
    


}
