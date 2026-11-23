using UnityEngine;
using UnityEngine.EventSystems;

public class Drop_Item : MonoBehaviour, IDropHandler
{
    [SerializeField] public Transform itemDrop;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {

            Vector3 itemDropPos_Original = itemDrop.position;
            GameObject parent_Original = itemDrop.parent.gameObject;

            itemDrop.SetParent(Drag.itemBeginDrag.parent.transform);
            itemDrop.position = Drag.itemBeginDrag.parent.position;

            Drag.itemBeginDrag.SetParent(parent_Original.transform);
            Drag.itemBeginDrag.position = itemDropPos_Original;
        }


    }
}
