using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class indicatorBehaviour : MonoBehaviour
{
    public Transform follow;

    private void Update()
    {
        transform.position = new Vector3(follow.position.x, transform.position.y, transform.position.z);   
    }
}
