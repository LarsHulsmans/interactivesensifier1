using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleContainer : MonoBehaviour
{
    private bool alreadyset = false;
    public void ScaleContent() 
    {
        if(transform.childCount > 0 || alreadyset == false) 
        {
            float width = transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.x;
            RectTransform thisTransform = GetComponent<RectTransform>();
            thisTransform.sizeDelta = new Vector2(width + 10, thisTransform.sizeDelta.y);
            alreadyset = true;
        }
    }
}