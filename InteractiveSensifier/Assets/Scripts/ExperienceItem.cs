using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;

public class ExperienceItem : MonoBehaviour
{
    public Text title;
    public Text time;
    public Image background;

    public Sprite DefaultImage;

    private SerializableSavedata _saveData;
    public SerializableSavedata Savedata 
    {
        get 
        {
            return _saveData;
        }
        set 
        {
            _saveData = value;
            
            title.text = _saveData.projectName;
            
            TimeSpan t = TimeSpan.FromSeconds(_saveData.videoLenght);
            time.text = string.Format("{0:mm\\:ss}", t);

            background.sprite = LoadImage(_saveData.pathToThumbnail);
        }
    }

    private Sprite LoadImage(string url) 
    {
        if (!File.Exists(url)) 
        {
            return DefaultImage;
        }

        byte[] imgData;
        Texture2D tex = new Texture2D(2, 2);

        
        imgData = File.ReadAllBytes(url);

        tex.LoadImage(imgData);

        Vector2 pivot = new Vector2(0.5f, 0.5f);
        Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), pivot, 100);

        return sprite;
    }
}