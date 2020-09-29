using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlstripControls : MonoBehaviour
{
    public GameObject PlayButton;
    public GameObject PauseButton;
    public Slider VolumeSlider;
    public Slider VideoTimeSlider;

    public Text videoTime;

    public void Play() 
    {
        PauseButton.SetActive(false);
        PlayButton.SetActive(true);
    }

    public void Pause() 
    {
        PlayButton.SetActive(false);
        PauseButton.SetActive(true);
    }

    public void ChangeVolume() 
    {
        
    }

    public void ToggleMute() 
    {
    
    }

    public void ScrubTimeline() 
    {
        
    }

    public void ChangeVideoTime(string totalTime, string currentTime) 
    {
        string timeString = totalTime + " / " + currentTime;

        videoTime.text = timeString;
    }

}
