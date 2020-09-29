using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using UnityEngine.UI;


public enum fileType 
{
    jpg,
    mp4
}

public class SelectFile : MonoBehaviour
{
    public fileType fType;

    public string filePath;

    public Text pathText;

    public CreateNewProject projectManager;

    public void SelectFileByType() 
    {
        string typestring = "";

        switch (fType) 
        {
            case fileType.jpg:
                typestring = "jpg";
                break;
            case fileType.mp4:
                typestring = "mp4";
                break;
        }

        filePath = EditorUtility.OpenFilePanel("Select file", "", typestring);
        
        if (!string.IsNullOrEmpty(filePath)) 
        {
            pathText.text = filePath;
            switch (fType)
            {
                case fileType.jpg:
                    projectManager.SetThumbnail(filePath);
                    break;
                case fileType.mp4:
                    projectManager.SetVideo(filePath);
                    break;
            }
        }
    }
}