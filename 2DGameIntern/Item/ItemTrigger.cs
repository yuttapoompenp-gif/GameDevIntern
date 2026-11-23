using UnityEngine;

public class ItemTrigger : MonoBehaviour
{


    [SerializeField] public GameObject item;
    [SerializeField] GameObject Button;
    
    public bool itemTriggered;
    public static bool collect_Item;

    private void Awake()
    {
        Button.SetActive(false);    
       
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Button.SetActive(true);

            itemTriggered = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            itemTriggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Button.SetActive(false);
            
            itemTriggered = false;
        }
    }
}
