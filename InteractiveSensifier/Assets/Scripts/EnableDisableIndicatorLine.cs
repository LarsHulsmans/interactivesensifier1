using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableDisableIndicatorLine : MonoBehaviour
{
    public RectTransform trans;
    public RectTransform bounds;

    public Image img;

    public PageManager pages;

    private float boundsMin;
    private float boundsMax;

    private bool _invisible = false;
    private bool invisible 
    {
        get 
        {
            return _invisible;
        }
        set 
        {
            if(value != _invisible) 
            {
                _invisible = value;
                SetRenderingOrder(_invisible);
            }
        }
    }

    private void Start()
    {
        float width = bounds.rect.width;
        float boundssize = width;


        boundsMin = bounds.rect.center.x - (width / 2);
        boundsMax = bounds.rect.center.x + (width / 2);
    }

    private void Update()
    {
        if (!pages.busy && pages.activePageIndex == 1) 
        {
            if (trans.transform.position.x > boundsMax)
            {
                invisible = true;
            }
            else if (trans.transform.position.x < boundsMin)
            {
                invisible = true;
            }
            else 
            {
                invisible = false;
            }
        }  
    }

    private void SetRenderingOrder(bool invis) 
    {
        if (invis) 
        {
            img.enabled = false;
        }
        else 
        {
            img.enabled = true;
        }
    }

}
