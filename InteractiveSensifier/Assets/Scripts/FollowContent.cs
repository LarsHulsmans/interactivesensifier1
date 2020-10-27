using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowContent : MonoBehaviour
{
    public direction followDirection;

    public RectTransform followObject;
    private RectTransform thisTransform;

    private float deltaposX = 0;
    private float deltaposY = 0;

    public float startY;

    private void Start()
    {
        thisTransform = GetComponent<RectTransform>();
        deltaposY = followObject.transform.position.y;
        deltaposX = followObject.transform.position.x;
        //thisTransform.localPosition = new Vector3(1170, thisTransform.localPosition.y, 0);
    }

    private void Update()
    {
        switch (followDirection)
        {
            case direction.X:
                thisTransform.transform.localPosition += new Vector3(followObject.transform.position.x - deltaposX, 0, 0);
                deltaposX = followObject.transform.position.x;
                break;
            case direction.Y:
                thisTransform.transform.localPosition += new Vector3(0, followObject.transform.position.y - deltaposY, 0);
                deltaposY = followObject.transform.position.y;
                break;
            case direction.XY:
                thisTransform.transform.localPosition += new Vector3(followObject.transform.position.x - deltaposX, followObject.transform.position.y - deltaposY, 0);
                deltaposY = followObject.transform.position.y;
                deltaposX = followObject.transform.position.x;
                break;
        }
    }
}