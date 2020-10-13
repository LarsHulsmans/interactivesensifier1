using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum direction { X, Y, XY}

public class ScaleToConent : MonoBehaviour
{
    public direction scaleDirection;

    public bool delayedStart = false;

    private RectTransform thisTransform;

    private void Start()
    {
        thisTransform = GetComponent<RectTransform>();

        if (delayedStart)
        {
            StartCoroutine(DelayedStart());
        }
        else 
        {
            Setup();
        }
    }

    private IEnumerator DelayedStart() 
    {
        yield return new WaitForSeconds(0.2f);
        Setup();
    }

    public void Setup() 
    {
        switch (scaleDirection) 
        {
            case direction.X:
                thisTransform.sizeDelta = new Vector2(GetXScale(), thisTransform.sizeDelta.y);
                break;
            case direction.Y:
                thisTransform.sizeDelta = new Vector2(thisTransform.sizeDelta.x, GetYScale());
                break;
            case direction.XY:
                thisTransform.sizeDelta = new Vector2(GetXScale(), GetYScale());
                break;
        }   
    }

    private float GetXScale() 
    {
        float maxWidth = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            RectTransform childTrans = transform.GetChild(i).GetComponent<RectTransform>();
            if(childTrans.sizeDelta.x > maxWidth) 
            {
                maxWidth = childTrans.sizeDelta.x;
            }  
        }

        return maxWidth;
    }

    private float GetYScale() 
    {
        float paddingY = GetComponent<VerticalLayoutGroup>().padding.top + GetComponent<VerticalLayoutGroup>().padding.bottom;
        float totalHeight = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            RectTransform childTrans = transform.GetChild(i).GetComponent<RectTransform>();
            totalHeight += childTrans.sizeDelta.y;
            totalHeight += paddingY;
        }

        return totalHeight;
    }

}
