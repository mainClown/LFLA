using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MiniGamePlace : MonoBehaviour, IDropHandler
{
    public int Id;
    public int ItemId;
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            ItemId = eventData.pointerDrag.GetComponent<MiniGameItem>().Id;
        }
    }
}
