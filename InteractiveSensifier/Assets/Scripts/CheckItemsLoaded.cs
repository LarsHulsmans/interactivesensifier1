using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckItemsLoaded : MonoBehaviour
{
    public bool SavesLoaded = false;


    public void Update()
    {
        if(SavesLoaded == true) 
        {
            int buildindex = SceneManager.GetActiveScene().buildIndex;
            if(SceneManager.sceneCount > buildindex) 
            {
                SceneManager.LoadScene(buildindex + 1);
            }
            else
            {
                Debug.LogError("scene not available");
            }
        }
    }

}
