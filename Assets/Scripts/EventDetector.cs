using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventDetector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Action<PointerEventData> OnPointerDownAction;
    public Action OnPointerUpAction;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDownAction?.Invoke(eventData);
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        OnPointerUpAction?.Invoke();
    }
}
