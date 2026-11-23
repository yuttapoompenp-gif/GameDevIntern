using UnityEngine;
using UnityEngine.EventSystems;

public class Drop_Slot : MonoBehaviour, IDropHandler
{
    public static bool isDrop;

    public void OnDrop(PointerEventData eventData)
    {
        if (Drag.onDrag)
        {
            isDrop = true;

            if (!Item)
            {
                Drag.itemBeginDrag.SetParent(transform);
                Drag.itemBeginDrag.position = gameObject.transform.position;

            }
            else
            {
                Drag.itemBeginDrag.position = Drag.originPos;
            }
        }


    }

    public GameObject Item

    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            else

            {
                return null;
            }
        }

    }
}
    