using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleContainer : MonoBehaviour
{
    private bool alreadyset = false;
    public void ScaleContent() 
    {
        if(transform.childCount > 0 || alreadyset == false) 
        {
            VerticalLayoutGroup group = GetComponent<VerticalLayoutGroup>();

            float width = transform.GetChild(1).GetComponent<RectTransform>().sizeDelta.x;
            RectTransform thisTransform = GetComponent<RectTransform>();

            float height = 0;
            for (int i = 0; i < transform.childCount; i++)
            {
                height += transform.GetChild(i).GetComponent<RectTransform>().sizeDelta.y;
                height += group.padding.top;
            }

            thisTransform.sizeDelta = new Vector2(width + 10, height);
            alreadyset = true;
        }
    }
}