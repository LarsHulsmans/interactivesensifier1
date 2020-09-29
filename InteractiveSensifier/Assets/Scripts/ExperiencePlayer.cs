using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RenderHeads.Media.AVProVideo;
using Sensiks;
using LitJson;
using System;

public class ExperiencePlayer : MonoBehaviour
{
    public MediaPlayer mediaPlayer;
    private SerializableSavedata data;

    private int experienceLenght;
    private string experienceLenghtText;

    public ControlstripControls controls;

    private bool isPlaying = false;

    

    public void Start()
    {
        StartCoroutine(CheckForSavesManager());
    }

    private IEnumerator CheckForSavesManager() 
    {
        while(data == null) 
        {
            try 
            {
                data = SavesManager.Instance.selectedData;    
            }
            catch 
            {
            }
            yield return null;
        }
        Startup();
    }

    private void Startup() 
    {
        mediaPlayer.OpenVideoFromFile(MediaPlayer.FileLocation.AbsolutePathOrURL, data.pathToVideo, false);

        experienceLenght = data.videoLenght;
        TimeSpan explenght = TimeSpan.FromSeconds(experienceLenght);
        experienceLenghtText = string.Format("{0:D2}m:{1:D2}s", explenght.Minutes, explenght.Seconds);
        controls.ChangeVideoTime("00:00", experienceLenghtText);


    }

    private void Update()
    {
        if (isPlaying) 
        {
            //update time text
            float tim = mediaPlayer.Control.GetCurrentTimeMs();
            TimeSpan temp = TimeSpan.FromMilliseconds(tim);  
            controls.ChangeVideoTime(string.Format("{0:D2}m:{1:D2}s", temp.Minutes, temp.Seconds), experienceLenghtText);


        }
    }
}
