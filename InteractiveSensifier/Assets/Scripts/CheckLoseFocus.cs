using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CheckLoseFocus : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public RectTransform rectTrans;

    private bool pointerOver = false;

    public UnityEvent OnOutsideClick;
    public UnityEvent OnInsideClick;

    public void Start()
    {
        if(rectTrans == null) 
        {
            RectTransform comp = gameObject.GetComponent<RectTransform>();
            if(comp != null) 
            {
                rectTrans = comp;
            }
        }
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            if (pointerOver) 
            {
                OnInsideClick.Invoke();
            }
            else 
            {
                OnOutsideClick.Invoke();
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        pointerOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pointerOver = false;
    }
}