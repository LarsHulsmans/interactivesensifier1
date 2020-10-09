using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimelineIndicatorSecond : MonoBehaviour
{
    public Text time;
    public Text timeHalf;
    public void Setup(int timestamp) 
    {
        time.text = SecondsToString(timestamp);
        timeHalf.text = SecondsToString(timestamp + 5);
    }

    private string SecondsToString(int time) 
    {
        float minutes = Mathf.Floor(time / 60);
        float seconds = Mathf.RoundToInt(time % 60);

        string timeText = minutes.ToString("00") + ":" + seconds.ToString("00");

       return timeText;
    }
}