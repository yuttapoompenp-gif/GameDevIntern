using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] RectTransform rectTransform;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] Image image;
    [SerializeField] GameObject backGround;
    
    public static Transform itemBeginDrag;

    public static Vector3 originPos;

    public static bool onDrag;

    private Color currentColor;
    private void Awake()
    {
        canvasGroup.blocksRaycasts = true;

    }



    public void OnBeginDrag(PointerEventData eventData)
    {

        Drop_Slot.isDrop = false;
        originPos = rectTransform.position;
        itemBeginDrag = gameObject.transform;
        backGround.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            onDrag = true;
            rectTransform.parent.transform.SetAsLastSibling();
            rectTransform.position = eventData.position;
            canvasGroup.blocksRaycasts = false;
            currentColor = image.color;
            currentColor.a = 0.5f;
            image.color = currentColor;

        }
        else
        {
            currentColor = image.color;
            onDrag = false;
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        currentColor.a = 1f;
        image.color = currentColor;

        canvasGroup.blocksRaycasts = true;
        if (!Drop_Slot.isDrop)
        {
            rectTransform.position = originPos;
        }
    }



}
