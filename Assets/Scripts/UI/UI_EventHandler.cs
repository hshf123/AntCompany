using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Action OnClickHandler = null;
    public Action OnPressedHandler = null;
    public Action OnDownHandler = null;
    public Action OnUpHandler = null;
    public Action<PointerEventData> OnDragHandler = null;

    bool _isPressed = false;

    void Update()
    {
        if (_isPressed)
            OnPressedHandler?.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnClickHandler != null)
            OnClickHandler?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPressed = true;
        if (OnDownHandler != null)
            OnDownHandler?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPressed = false;
        if (OnUpHandler != null)
            OnUpHandler?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (OnDragHandler != null)
            OnDragHandler?.Invoke(eventData);
    }
}
