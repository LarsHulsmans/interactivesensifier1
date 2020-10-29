using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RenderHeads.Media.AVProVideo;

public class VideoPlayerManager : MonoBehaviour
{
    public static VideoPlayerManager instance;

    public MediaPlayer mainPlayer;

    public float totalVideoLenght;
    public float curentVideoProgress;

    public bool isPlaying;

    private void Awake()
    {
        instance = this;
    }

    private void Setup()
    {

    }

    private void Update()
    {
        if(isPlaying)
        {
            curentVideoProgress += Time.deltaTime;
        }
    }    



}