using Sensiks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupContentHeight : MonoBehaviour
{
    void Start()
    {
        if(SavesManager.Instance == null) 
        {
            RectTransform rect = gameObject.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, 460);
            return;
        }
        int saves = SavesManager.Instance.savedata.Count;

        Debug.Log("ceil: " + Mathf.CeilToInt((float)saves / 3f));

        int height = Mathf.CeilToInt((float)saves / 3f) * 470;

        Debug.Log("height: " + height);


        RectTransform rt = gameObject.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, height);
    }

}
