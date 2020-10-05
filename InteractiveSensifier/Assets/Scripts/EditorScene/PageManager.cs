using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageManager : MonoBehaviour
{
    public GameObject[] Pages;
    public GameObject[] PageIndicators;

    public int activePageIndex = 0;

    public RectTransform pageIndicatorHolder;
    public GameObject pageIndicator;

    public bool sliding = false;

    private float deltaSlidingX;

    public float slidingMultiplier = 1;

    private Coroutine returnPage;

    private RectTransform thisTransform;

    private int currentStationaryPos = 0;

    private void Start()
    {
        currentStationaryPos = activePageIndex * -1080;
        thisTransform = GetComponent<RectTransform>();
        List<GameObject> indicators = new List<GameObject>();
        foreach(GameObject g in Pages) 
        {
            GameObject temp = Instantiate(pageIndicator);
            temp.transform.parent = pageIndicatorHolder;
            temp.transform.localPosition = new Vector2(0, 0);
            indicators.Add(temp);
        }
        PageIndicators = indicators.ToArray();

        for (int i = 0; i < PageIndicators.Length; i++)
        {
            if(i != activePageIndex) 
            {
                PageIndicators[i].GetComponent<PageIndicator>().SetPageInactive();
            }
            else 
            {
                PageIndicators[i].GetComponent<PageIndicator>().SetPageActive();
            }
        }
    }

    private void SetIndicators() 
    {
        for (int i = 0; i < PageIndicators.Length; i++)
        {
            if (i != activePageIndex)
            {
                PageIndicators[i].GetComponent<PageIndicator>().SetPageInactive();
            }
            else
            {
                PageIndicators[i].GetComponent<PageIndicator>().SetPageActive();
            }
        }
    }

    public void PointerUp() 
    {
        sliding = false;
        deltaSlidingX = 0;

        float xPosTo = 0;

        if (activePageIndex > 0) 
        {
            if(activePageIndex >= Pages.Length-1) 
            {
                //at last page
                xPosTo = Closer(currentStationaryPos, currentStationaryPos + 1080, (int)thisTransform.localPosition.x);
            }
            else 
            {
                //somewhere in between
                float xPosToPositive = Closer(currentStationaryPos, currentStationaryPos + 1080, (int)thisTransform.localPosition.x);
                float xPosToNegative = Closer(currentStationaryPos, currentStationaryPos - 1080, (int)thisTransform.localPosition.x);
                if(xPosToPositive != currentStationaryPos) 
                {
                    xPosTo = xPosToPositive;
                }
                else if(xPosToNegative != currentStationaryPos)
                {
                    xPosTo = xPosToNegative;
                }
                else 
                {
                    xPosTo = currentStationaryPos;
                }
            }
        }
        else 
        {
            //at first page
            xPosTo = Closer(currentStationaryPos, currentStationaryPos - 1080, (int)thisTransform.localPosition.x);
        }
        

        if(thisTransform.localPosition.x > xPosTo) 
        {
            returnPage = StartCoroutine(returnPageRoutine((int)xPosTo, false));
            if (activePageIndex != Pages.Length-1 && xPosTo != currentStationaryPos) 
            {
                activePageIndex++;
                SetIndicators();
            }    
        }
        else 
        {
            returnPage = StartCoroutine(returnPageRoutine((int)xPosTo, true));
            if (activePageIndex > 0 && xPosTo != currentStationaryPos) 
            {
                activePageIndex--;
                SetIndicators();
            }            
        }
    }

    public void PointerDown() 
    {
        sliding = true;
        deltaSlidingX = Input.mousePosition.x;
        if(returnPage != null) 
        {
            StopCoroutine(returnPage);
        }
    }

    private void Update()
    {
        if (sliding) 
        {
            float dist = ((deltaSlidingX - Input.mousePosition.x) * -1f) * slidingMultiplier;
            thisTransform.localPosition = new Vector2(thisTransform.localPosition.x + dist, thisTransform.localPosition.y);
            deltaSlidingX = Input.mousePosition.x;
        }
    }

    IEnumerator returnPageRoutine(int xPosTo, bool add) 
    {
        float posX = thisTransform.localPosition.x;
        float speed = 15;

        if(add)
        {
            //add
            while (posX < xPosTo)
            {
                posX += speed;
                thisTransform.localPosition = new Vector2(posX, thisTransform.localPosition.y);
                yield return null;
            }
        }
        else 
        {
            //subtract
            while (posX > xPosTo)
            {
                posX -= speed;
                thisTransform.localPosition = new Vector2(posX, thisTransform.localPosition.y);
                yield return null;
            }
        }

        thisTransform.localPosition = new Vector2(xPosTo, thisTransform.localPosition.y);
        currentStationaryPos = (int)thisTransform.localPosition.x;
        returnPage = null;
    }

    private int Closer(int a, int b, int compareValue)
    {
        int calcA = Math.Abs(a - compareValue);
        int calcB = Math.Abs(b - compareValue);

        if (calcA == calcB)
        {
            return 0;
        }

        if (calcA < calcB)
        {
            return a;
        }

        return b;
    }
}
