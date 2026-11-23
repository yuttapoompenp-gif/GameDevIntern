using UnityEngine;
using UnityEngine.EventSystems;

public class Pointer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] GameObject backGround;
    private void Awake()
    {
        backGround.SetActive(false);
    
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        backGround.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        if (backGround != null)
        {
            if(eventData.button == PointerEventData.InputButton.Right)
            {
                backGround.transform.parent.parent.SetAsLastSibling();
                backGround.SetActive(!backGround.activeSelf);
            }
        }
    } 



}
