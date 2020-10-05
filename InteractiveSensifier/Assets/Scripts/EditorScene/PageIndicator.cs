using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageIndicator : MonoBehaviour
{
    public GameObject child;
    public void SetPageActive() 
    {
        this.child.SetActive(true);
    }

    public void SetPageInactive() 
    {
        this.child.SetActive(false);
    }
}
